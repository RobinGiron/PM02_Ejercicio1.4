using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tarea1_4.Models
{
    public class IMG
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String titulo { get; set; }
        public String desc { get; set; }
        public Byte[] img { get; set; }

        public string lblid { get { return $"ID: {id}"; } }
        public string lbltitulo { get { return $"Titulo: {titulo}"; } }
        public string lbldesc { get { return $"Descripcion: {desc}"; } }
    }
}
