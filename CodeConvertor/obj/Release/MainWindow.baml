   M S B A M L   `   `   `  ���� H  DCodeConvertor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CodeConvertorCodeConvertor      CodeConvertor.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:CodeConvertor       � Title$)  #Числовой конвертер��$	��599��$
��1400�� � WindowStartupLocation$ CenterScreen=�$��Arial)�$��16(����	#FFE2E2E2$
��1400��$	��300�� � WindowState$ 	Maximized=�.���  � ColumnDefinitions �� -    ��Name$ InputRow��$��1* ��� -   $ 	OutputRow��$��1* � � RowDefinitions �� $��50 ��� -    ��Name$ 	MiddleRow��$��1* ��� $��50 �.+��� -    ��Name$ 	OpenInput��$��Открыть��$
��Left=�$��Center=�$��30��$��75��$��10,0,0,0q��� -   $ SaveAsInput��$��Сохранить как��$
��Left=�$��Center=�$��30��$	��125��$��0��$��95,0,0,0q��� -   $ 	SaveInput��$��Сохранить��$
��Left=�$��Center=�$��30��$	��100��$��0��$��	230,0,0,0q��� -   $ 	CopyInput��$��Копировать��$
��Left=�$��Center=�$��30��$	��100��$��2��$��10,0,0,0q��� -   $ InsertInput��$��Вставить��$
��Left=�$��Center=�$��30��$	��100��$��2��$��0��$��	120,0,0,0q��� -	   $ 
ClearInput��$��Очистить��$
��Left=�$��Center=�$��30��$	��100��$��2��$��0��$��	230,0,0,0q��� -
   $ SaveAsOutput��$��Сохранить как��$
��Left=�$��Center=�$��30��$	��125��$��1��$��10,0,0,0q��� -   $ 
SaveOutput��$��Сохранить��$
��Left=�$��Center=�$��30��$	��100��$��1��$��	145,0,0,0q��� -   $ 
CopyOutput��$��Копировать��$
��Left=�$��Center=�$��30��$	��100��$��2��$��1��$��10,0,0,0q��� -    ��Name$ 
InputCoder��$
��Left=�$��	340,0,0,0q�$��Center=�$	��170��$��30��$��Center=�$��Center=��� -   $ OutputCoder��$
��Left=�$��	255,0,0,0q�$��Center=�$	��170��$��30��$��1��$��Center=�$��Center=��� -   	 ��Name$	 InputTextBox��$��
10,25,10,0q�$��1��
 �� TextWrapping$

 Wrap=�$�� �� �� AcceptsReturn .  �� 
AcceptsTab . " �� VerticalScrollBarVisibility$
 Auto=��� -   $	 OutputTextBox��$��1��$

 Wrap=� .  . $��1�� �� 
IsReadOnly . $��
10,25,10,0q�$
 Auto=��� $��Поменять коды��$
��Left=�$��Center=�$��30��$	��125��$��	435,0,0,0q�$��1���� -    ��Name$ InputFileName��$
��Left=�$��	132,0,0,0q�$��1��$
��Wrap=�$��	input.txt��$	��Top=�$��20��$��65���� -   $ OutputFileName��$
��Left=�$��	140,0,0,0q�$
��Wrap=�$��
output.txt��$	��Top=�$��20��$��1��$��74��$��1���� $
��Left=�$��10,0,0,0q�$��1��$
��Wrap=�$��Входной файл:��$	��Top=�$��20��$	��117���� $��1��$
��Left=�$��10,0,0,0q�$��1��$
��Wrap=�$ ��Выходной файл:��$	��Top=�$��20��$	��125���� -   $ DelimeterTextBlock��$
��Left=�$��	520,0,0,0q�$
��Wrap=�$��Разделитель:��$��Center=�$��20��$	��100�� �� TextAlignment$ Center=��� -   $	 DelimiterString��$
��Left=�$��	625,0,0,0q�$

 Wrap=�$��Center=�$��60��$��20�� �� 	MaxLength$ 5�� �� MaxLines$ 1��