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
