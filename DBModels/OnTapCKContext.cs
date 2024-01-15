
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
        //this.Add
        //this.Update
        //this.Remove
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

    public List<CT_CB> GetHanhKhachs(string MACH){
        var HKs = this.CT_CB.Include(ct => ct.HANHKHACH).Where(ct => ct.MACH == MACH).ToList();
        return HKs;
    }

    public void InsertHK_CB(CT_CB cT_CB){
        this.Add(cT_CB);
        this.SaveChanges();
    }

    public HANHKHACH GetHANHKHACH(string MAHK){
        return this.HANHKHACH.Find(MAHK);
    }

    public CT_CB GetCT_CB(string MACH, string MAHK){
        return this.CT_CB.Where(ct => ct.MACH == MACH && ct.MAHK == MAHK).FirstOrDefault();
    }

    public void UpdateHK_CB(CT_CB cT_CB){
        this.Update(cT_CB);
        this.SaveChanges();
    }

    public void DeleteHK_CB(CT_CB cT_CB){
        this.Remove(cT_CB);
        this.SaveChanges();
    }

    DbSet<HANHKHACH> HANHKHACH { set; get; }

    DbSet<MAYBAY> MAYBAY { set; get; }

    DbSet<CHUYENBAY> CHUYENBAY { set; get; }

    DbSet<CT_CB> CT_CB { set; get; }
}