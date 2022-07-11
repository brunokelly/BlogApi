﻿using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campom deve conter entre 3 - 40 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O slug é obrigatório")]
        public string Slug { get; set; }
    }
}
