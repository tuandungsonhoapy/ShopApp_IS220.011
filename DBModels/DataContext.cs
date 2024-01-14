
using Ck_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

namespace DB_CK;

public class DataContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectString = configuration.GetConnectionString("DataContext") ?? "";
        builder.UseMySql(connectString, ServerVersion.AutoDetect(connectString));

        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NV_BT>()
            .HasKey(c => new { c.MaNhanVien, c.MaThietBi, c.MaCanHo });
    }

    public void InsertCH(CANHO cANHO){
        this.Add(cANHO);
        this.SaveChanges();
    }

    public List<Cau2> LietKeNV(int solansua){
        
        var result = (from nv in this.NHANVIENs
                      join nv_bt in this.NV_BTs
                      on nv.MaNhanVien equals nv_bt.MaNhanVien
                      group new {nv, nv_bt} by new {nv.MaNhanVien, nv.TenNhanVien, nv.SoDienThoai } into grouped
                      where grouped.Count() >= solansua
                      select new Cau2(){
                        TenNhanVien = grouped.Key.TenNhanVien,
                        SoDienThoai = grouped.Key.SoDienThoai,
                        SoLanSua = grouped.Count()
                      }).ToList();
        return result;
    }

    public List<NHANVIEN> GetNVs(){
        return this.NHANVIENs.ToList();
    }

    public List<NV_BT> GetNV_BTs(string MaNhanVien){
        return this.NV_BTs.Where(nv => nv.MaNhanVien == MaNhanVien).ToList();
    }

    public void DeleteNV_TB(NV_BT nV_BT){
        this.Remove(nV_BT);
        this.SaveChanges();
    }

    public NV_BT GetNV_BT(NV_BT nV_BT){
        return this.NV_BTs.Where(nv => nv.MaNhanVien == nV_BT.MaNhanVien && nv.MaThietBi == nV_BT.MaThietBi && nv.MaCanHo == nV_BT.MaCanHo && nv.LanThu == nV_BT.LanThu).FirstOrDefault();
    }

    public void UpdateNV_BT(NV_BT nV_BT){
        this.Update(nV_BT);
        this.SaveChanges();
    }

    DbSet<NHANVIEN> NHANVIENs { set; get; }

    DbSet<CANHO> cANHOs { set; get; }

    DbSet<THIETBI> THIETBIs { set; get; }

    DbSet<NV_BT> NV_BTs { set; get; }
}