//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurante_APP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Restaurante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurante()
        {
            this.Menu = new HashSet<Menu>();
        }
    
        public int restaurante_id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [RegularExpression(@"([A-Za-z�0-9�-��-������]*\ ?[A-Za-z�0-9�-��-������]+)+", ErrorMessage = "Caracteres permitidos: letras, n�meros e espa�o.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Digite pelo menos 1 caracter e no m�ximo 50.")]
        public string restaurante_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
