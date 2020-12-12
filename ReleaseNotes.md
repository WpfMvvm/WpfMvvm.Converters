# V.0.0.0.8 [27 ноября 2020г.]
Версия первой публикации темы [Библиотека элементов для реализации WPF MVVM Решений](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html).

В составе пакета конвертеры:
1.	[BooleanNotConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15042491) - логическое отрицание.
2.	[DefaultValueConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15042607) - дефолтное преобразование в другой тип.
3.	[ChainOfConverters](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15042653) - мутабельная цепочка конвертеров.
4.	[ReadOnlyChainOfConverters](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15042653) - иммутабельная цепочка конвертеров.
5.	[BooleanToVisibilityConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15042696) - bool в видимость и коллапс.
6.	[BooleanToVisibilityHiddenConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15042696) - bool в видимость и скрытие.
7.	[TraceConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15043503) - трассировка привязок.
8.	[EnumValuesConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15043512) - получение списка значений перечисления.
9.	[EqualsConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15043529) - сравнение значения с параметром.
10.	[GetTypeConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15043541) - получение типа значения.
11.	[DictionaryConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15043607) - конвертация значения по словарю.
12.	[DictionaryTypeConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15043607) - конвертация типа значения по словарю.

# V.0.0.0.9 [28 ноября 2020г.]

Изменена реализация EqualsConverter: в случае если значение и параметр имеют разные типы, то перед сравнением значение преобразуется к типу параметра через DefaultValueConverter.

Добавлен конвертер:

13. [ExpressionConverter](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15045377) - вычисление простых арифметических выражений.
