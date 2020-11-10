using System;
using System.ComponentModel;

namespace WpfMvvm.Converters
{
    public partial class DictionaryConverterExtension : ISupportInitialize
    {
        /// <summary>Объект находится в состояни инициализации.</summary>
        public bool IsInit { get; private set; }

        /// <summary>Подаёт сигнал объекту о начале инициализации.</summary>
        public void BeginInit() => IsInit = true;

        /// <summary>Подает объекту сигнал о завершении инициализации.</summary>
        /// <exception cref="Exception">Если по завершении инициализации в словаре <see cref="Dictionary"/>
        /// есть элементы и одновременно задана привязка в <see cref="Binding"/>.<br/>
        /// С сообщением $"Нельзя одновремено задавать элементы словарю {nameof(Dictionary)} и привязку {nameof(Binding)}."</exception>
        public void EndInit()
        {
            if (Dictionary.Count > 0 && Binding != null)
                throw new Exception($"Нельзя одновремено задавать элементы словарю {nameof(Dictionary)} и привязку {nameof(Binding)}.");

            IsInit = false;
        }
    }
}
