using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraneeLibrary
{
   
    public abstract class SyncEntity
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public string Id { get; set; } = Guid.NewGuid().ToString();

        
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        public bool IsDirty { get; set; } = true;

        
        public bool IsDeleted { get; set; } = false;
    }
}