namespace PMB.Models
{
    /// Model data untuk menyimpan informasi pelamar
    public class Applicant
    {
        public string Name { get; set; }         // Nama lengkap pelamar
        public string SchoolOrigin { get; set; }  // Asal sekolah
        public int MathScore { get; set; }       // Nilai ujian matematika
        public string BankAccount { get; set; }  // Nomor rekening bank
    }
}