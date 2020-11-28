using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер вычисляющий арифметическое выражение заданное в строке.</summary>
    /// <remarks>Выражение задаётся или в параметре конвертера, или в первой привязке.<br/>
    /// В выражении могут быть заданы вставляемые значения.<br/>
    /// Для вставки значений используется метод <see cref="String.Format(string, object[])"/> для которого выражение используется как составной формат.<br/>
    /// Если выражение задано в первой привязке, то индекс 0 используется для него, и не должен использоваться в выражении.<para/>
    /// Для вычисления значения выражения используется метод <see cref="DataTable.Compute(string, string)"/>.</remarks>
    [ValueConversion(typeof(object), typeof(object))]
    [ValueConversion(typeof(object[]), typeof(object))]
    public class ExpressionConverter : WithoutConvertBackMultiConverter, IValueConverter
    {
        /// <summary>Приватный экземпляр таблицы используемой для вычисления выражения.</summary>
        private readonly DataTable dataTable = new DataTable();

        /// <summary>Возвращает значение выражения.</summary>
        /// <param name="values">Привязки значений для вставки в выражение. 
        /// В первой привязке может быть передано выражение.</param>
        /// <param name="targetType">Целевой тип. Не используется.</param>
        /// <param name="parameter">Параметр конвертера. Может быть использован для выражения.</param>
        /// <param name="culture">Культура. Не используется.</param>
        /// <returns>Возвращает результат выполнения метода <see cref="DataTable.Compute(string, string)"/> для полученного выражения.</returns>
        /// <remarks>Выражение передаётся в строке в параметре или (если нет параметра) в первой привязке.<br/>
        /// Если кроме выражения привязки не были переданы, то вычисляется само выражение.<br/>
        /// Если есть ещё привязки, то их значение вставляется в выражение методом <see cref="String.Format(string, object[])"/>.<br/>
        /// Если выражение было получено из привязки, то индекс 0 не должен использоваться в составном формате.</remarks>
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string expression;
                if (values == null || values.Length == 0)
                    expression = (string)parameter;

                else if (parameter != null)
                    expression = string.Format((string)parameter, values);

                else if (values.Length == 1)
                    expression = (string)values[0];
                else
                    expression = string.Format((string)values[0], values);

                object result = dataTable.Compute(expression, "");

                return StaticMethodsOfConverters
                    .GetDefaultValueConverter(result.GetType(), targetType, false)
                    .Convert(result, targetType, null, culture);
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <summary>Возвращает значение выражения.</summary>
        /// <param name="value">Привязка значения для вставки в выражение. 
        /// В привязке может быть передано выражение.</param>
        /// <param name="targetType">Целевой тип. Не используется.</param>
        /// <param name="parameter">Параметр конвертера. Может быть использован для выражения.</param>
        /// <param name="culture">Культура. Не используется.</param>
        /// <returns>Возвращает результат выполнения метода <see cref="DataTable.Compute(string, string)"/> для полученного выражения.</returns>
        /// <remarks>Выражение передаётся в строке в параметре или (если нет параметра) в привязке.<br/>
        /// Если в привязке выражение, то вычисляется само выражение.<br/>
        /// Если выражение в параметре, то значение привязки вставляется в выражение методом <see cref="String.Format(string, object[])"/>.<br/>
        /// В выражение будет использован только индекс 0.</remarks>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string expression;
                if (value == null)
                    expression = (string)parameter;

                else if (parameter != null)
                    expression = string.Format((string)parameter, value);

                else 
                    expression = (string)value;

                object result = dataTable.Compute(expression, "");

                return StaticMethodsOfConverters
                    .GetDefaultValueConverter(result.GetType(), targetType, false)
                    .Convert(result, targetType, null, culture);
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <inheritdoc cref="WithoutConvertBackMultiConverter.ConvertBack(object, Type[], object, CultureInfo)"/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => base.ConvertBack(value, new Type[] { targetType }, parameter, culture)[0];

        /// <summary>Экземпляр конвертера.</summary>
        public static ExpressionConverter Instance { get; } = new ExpressionConverter();

    }
}
