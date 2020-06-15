using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Database.Entities;

namespace Desafio.Database.Seed
{
    public static class Seed
    {
        public static void SeedModel(this ModelBuilder builder)
        {
            var adm = Guid.NewGuid();
            var colaborador = Guid.NewGuid();
            var usuario = Guid.NewGuid();
            //builder.Entity<UserProfileEntity>().HasData(
            //        new UserProfileEntity { id = adm, profile_desc = "Administrador", fl_ativo = true },
            //        new UserProfileEntity { id = colaborador, profile_desc = "Colaborador", fl_ativo = true },
            //        new UserProfileEntity { id = usuario, profile_desc = "Usuário", fl_ativo = true }
            //    );

            //builder.Entity<BlogUserEntity>().HasData(
            //        new BlogUserEntity { id = Guid.NewGuid(), user_name = "Adm", hashed_password = "123qwe!@#", id_user_profile = adm, fl_ativo = true }
            //    );
        }
    }
}
