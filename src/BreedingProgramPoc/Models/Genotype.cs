namespace BreedingProgramPoc.Models;

public abstract record Genotype<T>(Allele<T> A, Allele<T> B)
{
	public abstract T GetResult();

	public void Deconstruct(out Allele<T> A, out Allele<T> B)
	{
		A = this.A;
		B = this.B;
	}

	public abstract Genotype<T> ConstructNew(Allele<T> A, Allele<T> B);
}