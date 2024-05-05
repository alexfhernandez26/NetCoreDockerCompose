namespace microservices.Entities
{
    public class Post : Base
    {
        public string Titulo { get; set; }
        public Guid CategoriaId { get; set; }
        public Categorias Categorias { get; set; }
        public string Contenido { get; set; }

    }
}
