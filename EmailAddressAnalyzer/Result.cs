using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailAddressAnalyzer
{
    // Класс для передачи результата в интерфейс пользователя
    public class Result
    {
        // Хранит позицию ошибки (-1, если все корректно)
        private int _ErrPosition;
        // Хранит ошибку 
        private Err _Err;
        // Список имен доменов
        private static LinkedList<string> _ListOfDomains;

        // Конструктор
        public Result(int ErrPosition, Err Err, LinkedList<string> ListOfDomains)
        {
            _ErrPosition = ErrPosition;
            _Err = Err;
            _ListOfDomains = ListOfDomains;
        }

        // Свойство к позиции ошибки
        public int ErrPosition
        {
            get
            {
                return _ErrPosition;
            }
        }
        
        // Свойство к описанию ошибки
        public string ErrMessage
        {
            get
            {
                switch (_Err)
                {
                    case Err.NoError: { return "Нет ошибок."; }
                    case Err.OutOfRange: { return "Выход за границы анализируемой строки."; }
                    case Err.LetterExpected: { return "Ожидается буква."; }
                    case Err.LetterDigitExpected: { return "Ожидается буква или цифра."; }
                    case Err.DotHyphenLetterDigitAtExpected: { return "Ожидается буква, цифра, точка, дефис или эт."; }
                    case Err.DotHyphenLetterDigitExpected: { return "Ожидается буква, цифра, точка или дефис."; }
                    case Err.OverflowDomains: { return "Количество доменов превышено."; }
                    case Err.UnderflowDomains: { return "Количество доменов мало."; }
                    case Err.UnrecognizedError: { return "Неизвестная ошибка."; }
                    default: { return "Неизвестная ошибка."; }
                }
            }
        }

        // Свойство к списку имен доменов
        public LinkedList<string> ListOfDomains
        {
            get
            {
                return _ListOfDomains;
            }
        }
    }

    // Перечисление с типами ошибок
    public enum Err
    {
        NoError,                            // нет ошибок
        OutOfRange,                         // выход за границы анализируемой строки
        LetterExpected,                     // ожидается буква
        LetterDigitExpected,                // ожидается буква или цифра
        DotHyphenLetterDigitAtExpected,     // ожидается буква, цифра, точка, дефис или эт
        DotHyphenLetterDigitExpected,       // ожидается буква, цифра, точка или дефис
        OverflowDomains,                    // количество доменов превышено
        UnderflowDomains,                   // количество доменов мало
        UnrecognizedError                   // неизвестная ошибка
    }
}
