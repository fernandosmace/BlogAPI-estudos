using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels.Categories
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O campo Name deve conter entre 3 e 40 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Slug é obrigatório.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "O campo Slug deve conter entre 1 e 40 caracteres.")]
        public string Slug { get; set; }
    }
}