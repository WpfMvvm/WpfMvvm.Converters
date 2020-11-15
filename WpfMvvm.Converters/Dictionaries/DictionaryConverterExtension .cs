using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    // Реализация MarkupExtension

    /// <summary>Возвращает <see cref="DictionaryConverter.Instance"/> 
    /// или создаёт экземпляр конвертера со словарём с заданными типами ключа и значения.</summary>
    /// <remarks>Нельзя одновремено задавать элементы словарю <see cref="Dictionary"/> и привязку <see cref="Binding"/>.<br/>
    /// Если не заданы элементы <see cref="Dictionary"/> и привязка <see cref="Binding"/>, то возвращается один из статических экземпляров:
    /// <see cref="DictionaryConverter.Instance"/>, <see cref="DictionaryTypeConverter.InstanceUseBaseTypes"/>
    /// или <see cref="DictionaryTypeConverter.InstanceNotUseBaseTypes"/>.</remarks>
    [MarkupExtensionReturnType(typeof(DictionaryConverter))]
    public partial class DictionaryConverterExtension : MarkupExtension
    {
        /// <summary>Возвращает <see cref="DictionaryConverter.Instance"/> 
        /// или создаёт экземпляр конвертера со словарём с заданными типами ключа и значения.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Если не заданно значение <see cref="Binding"/> и элементы словаря <see cref="Dictionary"/>,
        /// то возвращает для значений <see cref="UseBasicTypes"/>:<br/>
        /// <see langword="null"/> - <see cref="DictionaryConverter.Instance"/>,<br/>
        /// <see langword="true"/> - <see cref="DictionaryTypeConverter.InstanceUseBaseTypes"/>,
        /// <see langword="false"/> - <see cref="DictionaryTypeConverter.InstanceNotUseBaseTypes"/>.<para/>
        /// Иначе возвращает для значений <see cref="UseBasicTypes"/>:<br/>
        /// <see langword="null"/> - новый эеземпляр <see cref="DictionaryConverter"/>,<br/>
        /// <see langword="true"/> и <see langword="false"/> - новый экземпляр <see cref="DictionaryTypeConverter"/> с <see cref="DictionaryTypeConverter.UseBasicTypes"/>=<see cref="UseBasicTypes"/>.<br/>
        /// В экземплярах конвертеров в свойство <see cref="DictionaryConverter.Dictionary"/> либо устанавливается словарь из <see cref="Dictionary"/>, 
        /// либо это свойство привязывается по привязке <see cref="Binding"/>.</returns>
        /// <exception cref="Exception">Если не закончена инициализация объекта. С сообщением "Инициализация объекта не завершена."<br/>
        /// Если в <see cref="UseBasicTypes"/> неожидаемое значение. С сообщением $"Неожидаемое значение {nameof(UseBasicTypes)}={UseBasicTypes}".</exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (IsInit)
                throw new Exception("Инициализация объекта не завершена.");

            if (Dictionary.Count == 0 && Binding == null)
            {
                switch (UseBasicTypes)
                {
                    case null:
                        return DictionaryConverter.Instance;
                    case true:
                        return DictionaryTypeConverter.InstanceUseBaseTypes;
                    case false:
                        return DictionaryTypeConverter.InstanceNotUseBaseTypes;
                    default:
                        throw new Exception($"Неожидаемое значение {nameof(UseBasicTypes)}={UseBasicTypes}");
                }
            }

            DictionaryConverter converter = UseBasicTypes == null
                ? new DictionaryConverter()
                : new DictionaryTypeConverter() { UseBasicTypes = UseBasicTypes.Value };

            if (Binding == null)
            {
                converter.Dictionary = Dictionary;
            }
            else
            {
                BindingOperations.SetBinding(converter, DictionaryConverter.DictionaryProperty, Binding);
            }

            return converter;
        }
    }
}
