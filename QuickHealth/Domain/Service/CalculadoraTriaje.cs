namespace QuickHealth.Domain.Service
{
    public static class CalculadoraTriaje
    {
        public static int Calcular(
        int frecuenciaCardiaca, double temperatura,
        int presionSistolica, int presionDiastolica)
        {
            int nivelFC = EvaluarFrecuenciaCardiaca(frecuenciaCardiaca);
            int nivelTemp = EvaluarTemperatura(temperatura);
            int nivelPresion = EvaluarPresion(presionSistolica, presionDiastolica);

            // El signo vital más crítico define la prioridad del paciente
            return Math.Min(nivelFC, Math.Min(nivelTemp, nivelPresion));
        }

        public static string ObtenerDescripcion(int nivel) => nivel switch
        {
            1 => "Inmediato",
            2 => "Muy urgente",
            3 => "Urgente",
            4 => "Menos urgente",
            5 => "No urgente",
            _ => "Desconocido"
        };

        private static int EvaluarFrecuenciaCardiaca(int fc) => fc switch
        {
            > 150 or < 40 => 1,
            > 120 or < 50 => 2,
            > 100 or < 55 => 3,
            > 90 or < 60 => 4,
            _ => 5
        };

        private static int EvaluarTemperatura(double temp) => temp switch
        {
            > 40.5 or < 35.0 => 1,
            > 39.5 or < 35.5 => 2,
            > 38.5 => 3,
            > 37.5 => 4,
            _ => 5
        };

        private static int EvaluarPresion(int sistolica, int diastolica)
        {
            if (sistolica > 180 || sistolica < 70) return 1;
            if (sistolica > 160 || diastolica > 110) return 2;
            if (sistolica > 140 || diastolica > 90) return 3;
            if (sistolica > 130 || diastolica > 85) return 4;
            return 5;
        }
    }
}
