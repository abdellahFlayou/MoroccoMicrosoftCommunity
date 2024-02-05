using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MoroccoMicrosoftCommunity.Domain.Models;

namespace MoroccoMicrosoftCommunity.Infrastructure.Data;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evenement> Evenements { get; set; }

    public virtual DbSet<Partenaire> Partenaires { get; set; }

    public virtual DbSet<PartenaireEvenement> PartenaireEvenements { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<ParticipementSession> ParticipementSessions { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Speaker> Speakers { get; set; }

    public virtual DbSet<SpeakerSession> SpeakerSessions { get; set; }

    public virtual DbSet<Sponsor> Sponsors { get; set; }

    public virtual DbSet<SponsorSession> SponsorSessions { get; set; }

    public virtual DbSet<Support> Supports { get; set; }

    public virtual DbSet<SupportSession> SupportSessions { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-95SP9V3\\SQLEXPRESS;Database=EvenementMoroccoMicrosoftCommunity;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evenement>(entity =>
        {
            entity.HasKey(e => e.EvenementId).HasName("PK__Evenemen__327074D04CDF446C");

            entity.ToTable("Evenement");

            entity.Property(e => e.EvenementId)
                .ValueGeneratedNever()
                .HasColumnName("EvenementID");
            entity.Property(e => e.DateDebut).HasColumnType("datetime");
            entity.Property(e => e.DateFin).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Titre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Partenaire>(entity =>
        {
            entity.HasKey(e => e.PartenaireId).HasName("PK__Partenai__5DCADE5D0FCE7C77");

            entity.ToTable("Partenaire");

            entity.Property(e => e.PartenaireId)
                .ValueGeneratedNever()
                .HasColumnName("PartenaireID");
            entity.Property(e => e.EvenementId).HasColumnName("EvenementID");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Evenement).WithMany(p => p.Partenaires)
                .HasForeignKey(d => d.EvenementId)
                .HasConstraintName("FK__Partenair__Evene__46E78A0C");
        });

        modelBuilder.Entity<PartenaireEvenement>(entity =>
        {
            entity.HasKey(e => e.PartenaireEvenementId).HasName("PK__Partenai__3C11F94B48006E65");

            entity.ToTable("PartenaireEvenement");

            entity.Property(e => e.PartenaireEvenementId)
                .ValueGeneratedNever()
                .HasColumnName("PartenaireEvenementID");
            entity.Property(e => e.EvenementId).HasColumnName("EvenementID");
            entity.Property(e => e.PartenaireId).HasColumnName("PartenaireID");

            entity.HasOne(d => d.Evenement).WithMany(p => p.PartenaireEvenements)
                .HasForeignKey(d => d.EvenementId)
                .HasConstraintName("FK__Partenair__Evene__70DDC3D8");

            entity.HasOne(d => d.Partenaire).WithMany(p => p.PartenaireEvenements)
                .HasForeignKey(d => d.PartenaireId)
                .HasConstraintName("FK__Partenair__Parte__6FE99F9F");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("PK__Particip__7227997E1E19450C");

            entity.ToTable("Participant");

            entity.Property(e => e.ParticipantId)
                .ValueGeneratedNever()
                .HasColumnName("ParticipantID");
            entity.Property(e => e.UtilisateurId).HasColumnName("UtilisateurID");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Participants)
                .HasForeignKey(d => d.UtilisateurId)
                .HasConstraintName("FK__Participa__Utili__5165187F");
        });

        modelBuilder.Entity<ParticipementSession>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ParticipementSession");

            entity.Property(e => e.ParticipantId).HasColumnName("ParticipantID");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");

            entity.HasOne(d => d.Participant).WithMany()
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK__Participe__Parti__534D60F1");

            entity.HasOne(d => d.Session).WithMany()
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__Participe__Sessi__5441852A");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Session__C9F492701448C075");

            entity.ToTable("Session");

            entity.Property(e => e.SessionId)
                .ValueGeneratedNever()
                .HasColumnName("SessionID");
            entity.Property(e => e.Adresse)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DateSession)
                .HasColumnType("datetime")
                .HasColumnName("dateSession");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EvenementId).HasColumnName("EvenementID");
            entity.Property(e => e.SpeakerId).HasColumnName("SpeakerID");
            entity.Property(e => e.TitreSession)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtilisateurId).HasColumnName("UtilisateurID");

            entity.HasOne(d => d.Evenement).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.EvenementId)
                .HasConstraintName("FK__Session__Eveneme__403A8C7D");

            entity.HasOne(d => d.Speaker).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.SpeakerId)
                .HasConstraintName("FK__Session__Speaker__3F466844");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UtilisateurId)
                .HasConstraintName("FK__Session__Utilisa__412EB0B6");
        });

        modelBuilder.Entity<Speaker>(entity =>
        {
            entity.HasKey(e => e.SpeakerId).HasName("PK__Speakers__79E75739178045D2");

            entity.Property(e => e.SpeakerId)
                .ValueGeneratedNever()
                .HasColumnName("SpeakerID");
            entity.Property(e => e.Biography).HasColumnType("text");
            entity.Property(e => e.LienFacebook)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LienInstagram)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LienLinkedin)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LienTwitter)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Mct).HasColumnName("MCT");
            entity.Property(e => e.Mvp).HasColumnName("MVP");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SiteWeb)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UtilisateurId).HasColumnName("UtilisateurID");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Speakers)
                .HasForeignKey(d => d.UtilisateurId)
                .HasConstraintName("FK__Speakers__Utilis__3C69FB99");
        });

        modelBuilder.Entity<SpeakerSession>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SpeakerSession");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.SpeakerId).HasColumnName("SpeakerID");

            entity.HasOne(d => d.Session).WithMany()
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__SpeakerSe__Sessi__4E88ABD4");

            entity.HasOne(d => d.Speaker).WithMany()
                .HasForeignKey(d => d.SpeakerId)
                .HasConstraintName("FK__SpeakerSe__Speak__4D94879B");
        });

        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.HasKey(e => e.SponsorId).HasName("PK__Sponsor__3B609EF55D8EA684");

            entity.ToTable("Sponsor");

            entity.Property(e => e.SponsorId)
                .ValueGeneratedNever()
                .HasColumnName("SponsorID");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SessionId).HasColumnName("SessionID");

            entity.HasOne(d => d.Session).WithMany(p => p.Sponsors)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__Sponsor__Session__440B1D61");
        });

        modelBuilder.Entity<SponsorSession>(entity =>
        {
            entity.HasKey(e => e.SponsorSessionId).HasName("PK__SponsorS__B265122B009EDA7F");

            entity.ToTable("SponsorSession");

            entity.Property(e => e.SponsorSessionId)
                .ValueGeneratedNever()
                .HasColumnName("SponsorSessionID");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.SponsorId).HasColumnName("SponsorID");

            entity.HasOne(d => d.Session).WithMany(p => p.SponsorSessions)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__SponsorSe__Sessi__74AE54BC");

            entity.HasOne(d => d.Sponsor).WithMany(p => p.SponsorSessions)
                .HasForeignKey(d => d.SponsorId)
                .HasConstraintName("FK__SponsorSe__Spons__73BA3083");
        });

        modelBuilder.Entity<Support>(entity =>
        {
            entity.HasKey(e => e.SupportId).HasName("PK__Support__D82DBC6C73ADE847");

            entity.ToTable("Support");

            entity.Property(e => e.SupportId)
                .ValueGeneratedNever()
                .HasColumnName("SupportID");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Statut)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupportSession>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SupportSession");

            entity.Property(e => e.DateSupportSession)
                .HasColumnType("datetime")
                .HasColumnName("dateSupportSession");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.SupportId).HasColumnName("SupportID");

            entity.HasOne(d => d.Session).WithMany()
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__SupportSe__Sessi__4BAC3F29");

            entity.HasOne(d => d.Support).WithMany()
                .HasForeignKey(d => d.SupportId)
                .HasConstraintName("FK__SupportSe__Suppo__4AB81AF0");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.UtilisateurId).HasName("PK__Utilisat__6CB6AE1F937C8FBF");

            entity.ToTable("Utilisateur");

            entity.HasIndex(e => e.AdresseMail, "UQ__Utilisat__F1D9A53D4786E995").IsUnique();

            entity.Property(e => e.UtilisateurId)
                .ValueGeneratedNever()
                .HasColumnName("UtilisateurID");
            entity.Property(e => e.AdresseMail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gsm)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("GSM");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexe)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Ville)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}