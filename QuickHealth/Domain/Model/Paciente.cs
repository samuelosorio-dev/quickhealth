using QuickHealth.Domain.Exception;
using QuickHealth.Domain.Service;

namespace QuickHealth.Domain.Model
{
    public class Paciente
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int Edad { get; private set; }
        public string Documento { get; private set; }
        public int FrecuenciaCardiaca { get; private set; }   
        public double Temperatura { get; private set; }      
        public int PresionSistolica { get; private set; }     
        public int PresionDiastolica { get; private set; }    
        public int NivelPrioridad { get; private set; }  
        public DateTime FechaRegistro { get; private set; }

        private Paciente() { }

        
        //Este metodo solo nos va servir para el repositorio en memoria
        internal void AsignarId(int id)
        {
            Id = id;
        }

        internal static Paciente Crear(
            string nombreCompleto, int edad, string documento,
            int frecuenciaCardiaca, double temperatura,
            int presionSistolica, int presionDiastolica)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new ExcepcionNegocio("El nombre del paciente es obligatorio.");
            if (edad <= 0 || edad > 120)
                throw new ExcepcionNegocio("La edad del paciente no es válida.");
            if (string.IsNullOrWhiteSpace(documento))
                throw new ExcepcionNegocio("El documento del paciente es obligatorio.");
            if (frecuenciaCardiaca <= 0)
                throw new ExcepcionNegocio("La frecuencia cardíaca debe ser mayor a 0.");
            if (temperatura <= 0)
                throw new ExcepcionNegocio("La temperatura debe ser mayor a 0.");
            if (presionSistolica <= 0 || presionDiastolica <= 0)
                throw new ExcepcionNegocio("Los valores de presión deben ser mayores a 0.");


            return new Paciente
            {
                Nombre = nombreCompleto,
                Edad = edad,
                Documento = documento,
                FrecuenciaCardiaca = frecuenciaCardiaca,
                Temperatura = temperatura,
                PresionSistolica = presionSistolica,
                PresionDiastolica = presionDiastolica,
                NivelPrioridad = CalculadoraTriaje.Calcular(frecuenciaCardiaca, temperatura, presionSistolica, presionDiastolica),
                FechaRegistro = DateTime.Now
            };
        }

        

        public void ActualizarSignosVitales(
        int frecuenciaCardiaca, double temperatura,
        int presionSistolica, int presionDiastolica)
        {
            if (frecuenciaCardiaca <= 0)
                throw new ExcepcionNegocio("La frecuencia cardíaca debe ser mayor a 0.");
            if (temperatura <= 0)
                throw new ExcepcionNegocio("La temperatura debe ser mayor a 0.");
            if (presionSistolica <= 0 || presionDiastolica <= 0)
                throw new ExcepcionNegocio("Los valores de presión deben ser mayores a 0.");

            FrecuenciaCardiaca = frecuenciaCardiaca;
            Temperatura = temperatura;
            PresionSistolica = presionSistolica;
            PresionDiastolica = presionDiastolica;
            NivelPrioridad = CalculadoraTriaje.Calcular(frecuenciaCardiaca, temperatura, presionSistolica, presionDiastolica);
        }
    }
}
