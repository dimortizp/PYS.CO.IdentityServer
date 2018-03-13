// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace IdentityServer4.Authorize.UI
{
    public class LoginInputModel
    {
        [Required(ErrorMessage ="Nombre de usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Contraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Recordar login")]
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}