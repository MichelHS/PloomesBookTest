using Microsoft.EntityFrameworkCore;
using Ploomes.ApiContext.Models;

namespace Ploomes.ApiContext.Context
{
    public partial class PloomesContext : DbContext
    {
        public PloomesContext()
        {
        }

        public PloomesContext(DbContextOptions<PloomesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookGenre> BookGenre { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookGenreId).HasColumnName("book_genre_id");

                entity.Property(e => e.FinishedReadingDate)
                    .HasColumnName("finished_reading_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("purchase_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartedReadingDate)
                    .HasColumnName("started_reading_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.StoppedPage).HasColumnName("stopped_page");

                entity.HasOne(d => d.BookGenre)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.BookGenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_book_genre");
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.ToTable("book_genre");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnName("genre")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
