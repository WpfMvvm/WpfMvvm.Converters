using System;
using System.ComponentModel;

namespace WpfMvvm.Converters
{
    public partial class DictionaryConverterExtension : ISupportInitialize
    {
        /// <summary>Объект находится в состояни инициализации.</summary>
        public bool IsInit { get; private set; }
        public void BeginInit() => IsInit = true;

        public void EndInit()
        {
            if (Dictionary.Count > 0 && Binding != null)
                throw new Exception($"Нельзя одновремено задавать элементы словарю {nameof(Dictionary)} и привязку {nameof(Binding)}.");

            IsInit = false;
        }
    }


}
