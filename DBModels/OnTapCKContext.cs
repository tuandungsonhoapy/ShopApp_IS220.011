
using CK_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace DB_CK;

public class OnTapCKContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectString = configuration.GetConnectionString("OnTapCKContext") ?? "";
        builder.UseMySql(connectString, ServerVersion.AutoDetect(connectString));

        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CT_CB>()
            .HasKey(c => new { c.MACH, c.MAHK });
    }

    private void ConfigureCT_CB(EntityTypeBuilder<CT_CB> builder)
    {
        builder.Property(e => e.LOAIGHE)
            .HasColumnType("tinyint(1)")
            .HasConversion<byte>();
    }

    public void InsertHK(HANHKHACH hANHKHACH){
        this.Add(hANHKHACH);
        this.SaveChanges();
    }

    public CHUYENBAY GetCHUYENBAY(string MACH){
        return this.CHUYENBAY.Find(MACH);
    }

    public int GetChoThuong(string MACH){
        return this.CT_CB.Where(ct => ct.MACH == MACH && ct.LOAIGHE==false).Count();
    }

    public int GetChoVIP(string MACH){
        return this.CT_CB.Where(ct => ct.MACH == MACH).Count(ct => ct.LOAIGHE);
    }

    public List<HK_CB> GetHanhKhachs(string MACH){
        var HKs = (from HK in this.HANHKHACH
                   join detail in this.CT_CB
                   on HK.MAHK equals detail.MAHK
                   where detail.MACH == MACH
                   select new HK_CB(){
                    MAHK = HK.MAHK,
                    HOTEN = HK.HOTEN,
                    SDT = HK.DIENTHOAI,
                    LOAIGHE = (detail.LOAIGHE == false) ? "Thuong" : "VIP",
                    SOGHE = detail.SOGHE
                   }).ToList();
        return HKs;
    }

    public void InsertHK_CB(CT_CB cT_CB){
        this.Add(cT_CB);
        this.SaveChanges();
    }

    DbSet<HANHKHACH> HANHKHACH { set; get; }

    DbSet<MAYBAY> MAYBAY { set; get; }

    DbSet<CHUYENBAY> CHUYENBAY { set; get; }

    DbSet<CT_CB> CT_CB { set; get; }
}