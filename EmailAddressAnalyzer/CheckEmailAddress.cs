using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailAddressAnalyzer
{
    // Статический класс для проверки адреса.
    static class CheckEmailAddress
    {
        // Перечисление состояний автомата
        private enum EnumState { Start, Error, Final, A, B, C, D, G };
        // Текущая позиция курсора в анализируемой строке 
        private static int _Pos;
        // Анализируемая строка
        private static string _Str;
        // Ошибка анализа
        private static Err _Err;
        // Позиция курсора ошибки в анализируемой строке
        private static int _ErrPos;
        // Список имен доменов
        private static LinkedList<string> _ListOfDomains;
        
        // Функция, реализующая проверку
        public static Result Check(string InputString)
        {
            _Pos = 0;
            _Str = InputString.ToUpper();
            _ListOfDomains = new LinkedList<string>();
            SetError(Err.NoError, -1);
            EmailAddress();
            return new Result(_ErrPos, _Err, _ListOfDomains);
        }

        // Реализация конечного автомата
        private static bool EmailAddress()
        {
            // Состояние автомата
            EnumState State = EnumState.Start;
            string DomainName = "";

            // Основной цикл ДКА
            while ((State != EnumState.Error) && (State != EnumState.Final))
            {
                // Если вышли за границы строки
                if (_Pos >= _Str.Length)
                {
                    SetError(Err.OutOfRange, _Pos - 1);
                    State = EnumState.Error;
                }
                else
                {
                    // Проверяем состояние ДКА
                    switch (State)
                    {
                        // Ожидаем букву
                        case EnumState.Start:
                            {
                                if (char.IsLetter(_Str[_Pos]))
                                {
                                    State = EnumState.A;
                                }
                                else
                                {
                                    SetError(Err.LetterExpected, _Pos);
                                    State = EnumState.Error;
                                }
                                break;
                            }
                        // Ожидаем букву, цифру, точку, дефис и эт
                        case EnumState.A:
                            {
                                if (char.IsLetterOrDigit(_Str[_Pos]))
                                {
                                    State = EnumState.A;
                                }
                                else if (_Str[_Pos] == '.' || _Str[_Pos] == '-')
                                {
                                    State = EnumState.B;
                                }
                                else if (_Str[_Pos] == '@')
                                {
                                    State = EnumState.C;
                                }
                                else
                                {
                                    SetError(Err.DotHyphenLetterDigitAtExpected, _Pos);
                                    State = EnumState.Error;
                                }
                                break;
                            }
                        // Ожидаем букву или цифру
                        case EnumState.B:
                            {
                                if (char.IsLetterOrDigit(_Str[_Pos]))
                                {
                                    State = EnumState.A;
                                }
                                else
                                {
                                    SetError(Err.LetterDigitExpected, _Pos);
                                    State = EnumState.Error;
                                }
                                break;
                            }
                        // Ожидаем букву
                        case EnumState.C:
                            {
                                if (char.IsLetter(_Str[_Pos]))
                                {
                                    // Накапливаем имя домена
                                    DomainName = _Str[_Pos].ToString();
                                    State = EnumState.D;
                                }
                                else
                                {
                                    SetError(Err.LetterExpected, _Pos);
                                    State = EnumState.Error;
                                }
                                break;
                            }
                        // Ожидаем букву, цифру, точку и дефис
                        case EnumState.D:
                            {
                                if (char.IsLetterOrDigit(_Str[_Pos]))
                                {
                                    // Накапливаем имя домена
                                    DomainName += _Str[_Pos];
                                    State = EnumState.D;
                                }
                                else if (_Str[_Pos] == '.')
                                {
                                    // Добавляем имя домена в список
                                    _ListOfDomains.AddLast(DomainName);
                                    // Проверяем превышение доменных имен
                                    // Можно остановить анализ раньше и не ждать 6-го доменного 
                                    // имени. Известно, что оно следует после точки.
                                    if (_ListOfDomains.Count >= 5)
                                    {
                                        SetError(Err.OverflowDomains, _Pos);
                                        State = EnumState.Error;
                                    }
                                    else
                                    {
                                        State = EnumState.C;
                                    }
                                }
                                else if (_Str[_Pos] == '-')
                                {
                                    // Накапливаем имя домена
                                    DomainName += '-';
                                    State = EnumState.G;
                                }
                                else
                                {
                                    // Добавляем имя домена в список
                                    _ListOfDomains.AddLast(DomainName);
                                    // Проверяем превышение доменных имен
                                    if (_ListOfDomains.Count > 5)
                                    {
                                        SetError(Err.OverflowDomains, _Pos);
                                        State = EnumState.Error;
                                    }
                                    // Проверяем недостачу доменных имен
                                    else if (_ListOfDomains.Count < 2)
                                    {
                                        SetError(Err.UnderflowDomains, _Pos);
                                        State = EnumState.Error;
                                    }
                                    else
                                    {
                                        State = EnumState.Final;
                                    }
                                }
                                break;
                            }
                        // Ожидаем букву или цифру
                        case EnumState.G:
                            {
                                if (char.IsLetterOrDigit(_Str[_Pos]))
                                {
                                    // Накапливаем имя домена
                                    DomainName += _Str[_Pos];
                                    State = EnumState.D;
                                }
                                else 
                                {
                                    SetError(Err.LetterDigitExpected, _Pos);
                                    State = EnumState.Error;
                                }
                                break;
                            }
                        // Если попали сюда, что-то не так
                        default:
                            {
                                State = EnumState.Error;
                                break;
                            }
                    }
                }
                _Pos++;
            }
            return (State == EnumState.Final);
        }

        // Установка типа и позиции ошибки
        private static void SetError(Err ErrorType, int ErrorPosition)
        {
            _Err = ErrorType;
            _ErrPos = ErrorPosition;
        }
    }
}
