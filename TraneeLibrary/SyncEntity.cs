using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraneeLibrary
{
    // Базовий клас для всіх сутностей, що потребують синхронізації
    public abstract class SyncEntity
    {
        // GUID як глобальний унікальний ключ для локальної та хмарної БД
        // Використовуємо string, щоб уникнути проблем із перетворенням у Guid.Empty.ToString()
        [Key] // Позначаємо як первинний ключ
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // EF не генерує його автоматично
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Мітка часу останньої зміни (для вирішення конфліктів)
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        // Чи був запис змінений локально (потрібно відправити на сервер)
        public bool IsDirty { get; set; } = true;

        // Чи був запис видалений локально (Soft Delete)
        public bool IsDeleted { get; set; } = false;
    }
}