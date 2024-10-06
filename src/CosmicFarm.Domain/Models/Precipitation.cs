namespace CosmicFarm.Domain.Models;

public class Precipitation
{
	public S S1 { get; set; }
	public S S2 { get; set; }
}

public class S
{
    public ScanTime ScanTime { get; set; }
    public SCStatus SCStatus { get; set; }
    public float[,] Longitude { get; set; }
    public float[,] Latitude { get; set; }
    public float[] SunLocalTime { get; set; }
    public int[] Quality { get; set; }
    public float[,] IncidenceAngle { get; set; }
    public int[,] SungGlintAngle { get; set; }
    public int[,] IncidenceAngleIndex { get; set; }
    public int[,] Tc { get; set; }
}
