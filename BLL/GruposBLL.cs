using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.BLL
{
    public class GruposBLL
    {
        public static bool Guardar(Grupos grupo)
        {
            if (!Existe(grupo.GrupoId))
                return Insertar(grupo);
            else
                return Modificar(grupo);
        }
        private static bool Insertar(Grupos grupo)
        {
            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {

                contexto.Grupos.Add(grupo);

                foreach (var detalle in grupo.Detalle)
                {
                    contexto.Entry(detalle.Persona).State = EntityState.Modified;

                    detalle.Persona.CantidadGrupos += 1;
                }

                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        private static bool Modificar(Grupos grupo)
        {
            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                var grupoAnterior = contexto.Grupos
                     .Where(x => x.GrupoId == grupo.GrupoId)
                     .Include(x => x.Detalle)
                     .ThenInclude(x => x.Persona)
                     .AsNoTracking()
                     .SingleOrDefault();


                foreach (var detalle in grupoAnterior.Detalle)
                {
                    contexto.Entry(detalle.Persona).State = EntityState.Modified;

                    detalle.Persona.CantidadGrupos -= 1;
                }

                contexto.Database.ExecuteSqlRaw($"Delete FROM GruposDetalle Where GrupoId={grupo.GrupoId}");

                foreach (var item in grupo.Detalle)
                {
                    contexto.Entry(item.Persona).State = EntityState.Modified;

                    item.Persona.CantidadGrupos += 1;

                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(grupo).State = EntityState.Modified;

                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {

                var grupo = GruposBLL.Buscar(id);

                if (grupo != null)
                {

                    foreach (var detalle in grupo.Detalle)
                    {
                        contexto.Entry(detalle.Persona).State = EntityState.Modified;

                        detalle.Persona.CantidadGrupos -= 1;
                    }

                    contexto.Grupos.Remove(grupo);

                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Grupos Buscar(int id)
        {
            Grupos grupo = new Grupos();

            Contexto contexto = new Contexto();

            try
            {
                grupo = contexto.Grupos.Include(x => x.Detalle)
                   .Where(x => x.GrupoId == id)
                   .Include(x => x.Detalle)
                   .ThenInclude(x => x.Persona)
                   .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return grupo;
        }
        public static List<Grupos> GetList(Expression<Func<Grupos, bool>> criterio)
        {
            List<Grupos> Lista = new List<Grupos>();

            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Grupos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();


            bool encontrado = false;

            try
            {
                encontrado = contexto.Grupos.Any(e => e.GrupoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
    }
}
