﻿// <auto-generated />
using System;
using GreenLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenLibrary.Data.Migrations
{
    [DbContext(typeof(GreenLibraryDbContext))]
    [Migration("20240422103229_SeedArticleAndCategoryTables")]
    partial class SeedArticleAndCategoryTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GreenLibrary.Data.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("88bf5f0f-9f9d-4356-97c3-550b1ab1664b"),
                            CategoryId = 2,
                            CreatedOn = new DateTime(2024, 4, 22, 10, 32, 29, 192, DateTimeKind.Utc).AddTicks(4992),
                            Description = "Пшеницата е на-важната селскостопанска култура в световен мащаб. Основното предназначение е за производството на брашно, а допълнителната продукция за изхранване на животните. Пшеницата произхожда от предна Азия (Кавказ). В България ежегодно се отглеждат 12-15 млн.дка, като средните добиви са от 400 до 700кг/дка. В световен мащаб площите на пшеницата намаляват, но добивите се увеличават. Най – големи добиви в Европа са Русия и Украйна, а в света – Кирай.\r\nБотаническа характеристика:\r\n1.	Коренова система – пшеницата формира брадеста коренова система, която се състои от 3-5 ембрионални корена и от 30-50 адвентивни корена. Дълбочината, на която достига кореновата системна е 30-40см.\r\n2.	Стъбло – то е с височина 60-150см, с възли и междувъзлия (те са от 4 до 6). Стъблото е кухо, правостоящо, склонно към полягане, особено при влажни години и торене с високи норми азот.\r\n3.	Листа – те са ланцетовидни и се формират до началото на цъфтежа.\r\n4.	Клас – той е характерен за житните култури и се състои от вретено като на всяко членче има от 3 до 7 цветчета. В зависимост от начина на отглеждане на 1 клас може да има от 20 до 60 зърна като теглото на 1 зърно е от 1 до 1,5гр.\r\n5.	Зърно – то се състои от обвивка, ендосперм и ядро. Съдържанието на вода е под 12%, като ендосперма и резервните хранителни в-ва заемат около 85% от теглото на зърното. При пшеницата различаваме маса на 1000 семена (абсолютно тегло). Това е теглото на 1000 сухи семена. При пшеницата теглото е в рамките на 35 до 50гр. Хектолитровата маса или тегло – това е един важен показател, който определя к-вото на брашно, което се получава. Най-качествено е теглото на 1000 литра зърно, което е с маса от 75 до 78 кг.\r\nБиологични изисквания:\r\n1.	Изисквания към топлина – през отделните си фази, тя изисква различно к-во топлина. Оптималната температура за растеж и развитие е от 10 до 20 градуса. Пшеницата издържа до -25 градуса минимална температура и до 30 градуса максимална. Над 32-35 градуса растенията страдат. Критичния период е фазата на вретенене и изкласяване. По време на вретененето оптималната температура е от 14 до 20 градуса, а по време на изкласяването 24-26 градуса.\r\n2.	Изисквания към светлина – пшеницата е растение на дългия ден. Вегетационния период е от 160 до 180 дни.\r\n3.	Изисквания към влага – тя се отнася към култури със средна взискателност към влага. За да поникнат семената трябва да погълнат 80 до 100% вода от собственото си тегло. Критичен период е времето на изкласяването във времето от 1 до 15 май. \r\n4.	Изисквания към почва – пшеницата е взискателна към почва. Тя се развива най-добре при черноземните и сивите горски почви с киселинност pH от 6 до 8. Неподходящи са тежките глинести и кисели почви. Пшеницата е взискателна към предшественика. Изискванията към него са всякакви – да бъдат от друга ботаническа група, да оставят площите чисти от плевели и др. Най-добри предшественици са бобовите култури, като грах, боб, фий, които обогатяват почвите. Подходящи са също така тютюн, картофи и памук. Неподходящи са житните култури като пшеница, ечемик и овес. В практиката често се налага засяване на пшеницата 2 или 3 години в сеидбооборот. \r\n5.	Обработка на почвата – пшеницата е взискателна към обработката на почвата. Обикновено обработката на почвата е диференциирана спряма предшественика. Така например след ранни предшественици като фасул и грах се извършва дълбока оран на дълбочина 23-25см с последващо култивиране на площта. След късни предшественици като тютюн, царевица и слънчоглед, които освобождават площите след 1 септември се извършват 2 или 3 обработки с дискови брани, като първата и втората обработка са на дълбочина 15-18см, а последната на 6-8см. целта е да се създаде здраво легло за семената. Една от най-късните култури, която се прибира е памука. Там е възможно на пречистени от плевели площи да се извърши и директна сеитба.\r\n6.	Торене – то е един от най-мощните фактори за получаване на високи добиви. За формирането на 100кг зърно, пшеницата извлича до 3.5кг азот; до 2кг фосфор и до 3кг калий. В БГ площите имат естествено плодородие, където без торене могат да се получат до 200-250кг зърно. Като примерни торови норми за получаването на 500-600кг зърно с необходимост да се внесат азот 14-16 активно в-во на дка, калий 10-12 кг/дка, фосфор 12-14 кг/дка. Цялото количество фосфор и калий се внася със есенната обработка, като 1/3 от азота се внася през есента и 2/3 за подхранване.\r\n7.	Сеитба – за сеитба се използват качествени семена с високо абсолютно тегло и кълняемост на семената над 95% и чистота 100%. Време – между 15 септември и 15 октомври. Норма – 550-600 кълняеми семена на кв.м. В килограми това е 25-28 кг семена/дка. Семената предварително се обеззаразяват срещу праховите и твърда главня. Дълбочина на сеитбата – 3-5см. След сеитбата в зависимост от влагата в почвата е небходимо да се извърши валиране.\r\n8.	Грижи през вегетацията – биват есенни, зимни и пролетни. Есенните грижи се състоят в борба с 2 неприятеля – хесенска муха и житен бегач. Обикновено те се появяват не всяка година, в топли есени. Вредата се състои в нагризване на младите листа. Борбата с тези неприятели се извежда чрез третиране с органо-фосфорни препарати. Пролетни мероприятия – след тежка зима, обикновено посева се изтегля и се извършва валиране, като целта е възела на въртене да остане под земята. До края на февруари трябва да се внесе останалото количество планиран азор\r\n9.	Борба с плевелите – срещу широколистни плевели се използват хербициди от групата на 2,4Д. Задължително хербицидите се внасят с листен тор за да може да се преодолее действието на хербицида. Основна грижа през вегетацията е борбата с болестите. Основно брашнеста мана. Борбата се извежда с препарати като Импакт в доза 80мл/дка. През вегетацията си пшеницата се напада от неприятели като житен бегач, житни бълхи, листни въшки и др. Борбата се извежда чрез третиране с органо-фосфорни препарати, като Нуреле Д 75мл/дка.\r\n10.	Прибиране – прибирането на пшеницата започва, когато влагата в семената е под 14%.\r\n",
                            Image = "wheat.jpg",
                            Title = "Пшеница",
                            UserId = new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726")
                        },
                        new
                        {
                            Id = new Guid("084c4100-846a-4536-a5c0-f512c4afdcd9"),
                            CategoryId = 2,
                            CreatedOn = new DateTime(2024, 4, 22, 10, 32, 29, 192, DateTimeKind.Utc).AddTicks(5011),
                            Description = "Памука е основна техническа култура – добитото влакно е основна суровина за леката промишленост. Семената съдържат от 20 до 30% мазнини и се използват за производството на глицерин, маргарин, сапуни и кюспе, който е отличен фураж за животните. Като окопна култура е отличен предшественик за повечето полски култури. В България се отглеждаше до 350-400хил.дка памук. Основно в Старозагорско, Хасковско и Пловдивско.\r\nБотаническа характеристика – памука е многогодишно храстовидно растение, което в България е едногодишна култура.\r\n1.	Коренова система – като цяло много добре развита с главен вретеновиден корен, който достига дълбочина до 150см със странични разклонения от 30 до 40см. Особеното в растежа е че първите 20-25дни расте много бавно – едва 3-4см.\r\n2.	Стъбло – то е правостоящо, в основата вдървесинено, на височина 100-110см. Стъблото е разклонено. При памука различаваме растежни клонки, които се развиват в началото много бързо и основно са по главното стъбло, и плодни клонки, които се развиват през втората част от вегетацията. Върху тях директно с дръжки са заловени цветовете, от които се образува плодната кутийка.\r\n3.	Листа – прости, покрити с власинки. От долната страна са по-едри, а в най-връхната част – дребни цветове. Съставени са от цветна дръжка и цветове, които могат да бъдат на цвят бели и кремави. Памука е самоопрашваща се култура.\r\n4.	Плодове – те представляват от 3 до 5 делни кутийки, закръглени. При узряване се разпукват. Масата на суровия памук в една кутийка е 0.50-1гр при дивите видове. При културните видове в 1 кутийка е от 10-12 гр памук. Влакното обикновено е единично и представлява разраснала се епидермална клетка на семепъпка до 2хил. пъти. Нарастването на влакното е процес, който продължава от 25 до 45дни след оплождането на семепъпката. Висококачествения памук е бял или кремав на цвят.\r\nБиологични изисквания\r\n1.	Изисквания към топлина – памука е топлолюбива култура. Семената поникват при температура 10-12 градуса на почвата. Оптимална температура е 26-31 градуса. Общата температурна сума е 3500-3800 градуса.\r\n2.	Изисквания към светлина – растение на късия ден.\r\n3.	Изисквания към влага – памука е взискателен, нищо че образува добра коренова система. Критичен период е началото на цъфтежа. В България има отлични условия за отглеждане на памук (главно Юж.България).\r\n4.	Изисквания към почва – памука изисква дълбоки, добре аерирани почви. Най – подходящи са черноземните почви.\r\nАгротехника \r\n1.	Място в сеитбообращението – памука може да се отглежда като монокултура в продължение на 4-5 години. Тоест не е взискателен към предшественика.\r\n2.	Обработка на почвата – диференцирана в зависимост от предшественика. При житни култури се извършва оран на дълбочина 25-30см. Когато памука е след памук орана на 20-25м.\r\n3.	Торене – примерни торови норми – азот 10-12кг/дка, фосфор 8кг/дка, калий 10-12кг/дка. Цялото количество фосфор и калий се внася преди дълбоката оран, а цялото количество азот преди сеитба с последната обработка. Памука е много отзивчив на торене с микроелементи (бор и манган).\r\n4.	Сеитба – 20-25 април. Междуредовото разстояние е 45см. Сеитбата се извършва на дълбочина 3-5см, а нормата е 3-3.5кг. семената/дка. В 1 дка трябва да има около 20-22хил растения.\r\nГрижи през вегетацията – състои се в разрушаване в началото на почвената кора.\r\n1.	Борба с плевели -  срещу едногодишни житни – Агрифлан 300мл/дка. През вегетацията срещу многогодишни житни (балур, троскот) Галъп 120мл/дка или Тарга 150мл/дка. При наличие на троскот или пирей дозата се удвоява.\r\n2.	Борба с болести – памука се напада от много болести, като бактериоза, антракноза, вертицилийно увяхване. Борбата срещу тези болести се извежда чрез обеззаразяване на семената със Апрон 120мл/100кг семена.\r\n3.	Борба с неприятели – от неприятелите (листни въшки, трипс, акар), борбата се извежда чрез 1 или 2 третирания по време на вегетацията. \r\nАгротехника – през вегетацията се извършват 2 или 3 окопавания. При възможност памука се полива и в тези случаи се получава много качествено влакно в рамките на 250-300кг памук/дка.\r\n4.	Прибиране – памуковите кутийки не узряват едновременно и не се разпукват едновременно. Ето защо едновременното прибиране е двукратно с памукокомбайн, когато над 65-70% са се разпукали. Втория път при пълно разпукване. В последно време се използват дефолианти или десеканти като целта е едновременно разпукване на всички кутийки. Разпукването започва около 15 септември.\r\n",
                            Image = "cotton.jpg",
                            Title = "Памук",
                            UserId = new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726")
                        });
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.ArticleLike", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticlesLikes");
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Зеленчукопроизводство"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Растениевъдство"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Вредители"
                        });
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModerator")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "74c6d229-f8b2-4bef-8c08-b0709bc289d4",
                            CreatedOn = new DateTime(2024, 4, 22, 10, 32, 29, 193, DateTimeKind.Utc).AddTicks(1577),
                            Email = "admin@test.com",
                            EmailConfirmed = true,
                            FirstName = "Георги",
                            IsDeleted = false,
                            IsModerator = false,
                            LastName = "Иванов",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@TEST.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAECFl1G/52VWwgHbKDkitgUCabd/GIPRfF1bSYd8qoayhTkzD3cfTFxd6w1nmVe0dIw==",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ea9456bc-ef07-41f8-ad8c-07ea338e674f",
                            CreatedOn = new DateTime(2024, 4, 22, 10, 32, 29, 237, DateTimeKind.Utc).AddTicks(7981),
                            Email = "moderator@test.com",
                            EmailConfirmed = true,
                            FirstName = "Елица",
                            IsDeleted = false,
                            IsModerator = false,
                            LastName = "Емилова",
                            LockoutEnabled = false,
                            NormalizedEmail = "MODERATOR@TEST.COM",
                            NormalizedUserName = "MODERATOR",
                            PasswordHash = "AQAAAAIAAYagAAAAECbNNmyFDfGv7mmAFdy0iJn9Yfzzz9KYJdKLS/8FInQ949W4UgXRLY5iXSKg6K4lQw==",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "moderator"
                        },
                        new
                        {
                            Id = new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "041c08a6-8ebc-4420-83e6-9eb4a418e3c0",
                            CreatedOn = new DateTime(2024, 4, 22, 10, 32, 29, 282, DateTimeKind.Utc).AddTicks(3013),
                            Email = "user@test.com",
                            EmailConfirmed = true,
                            FirstName = "Петър",
                            IsDeleted = false,
                            IsModerator = false,
                            LastName = "Петров",
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@TEST.COM",
                            NormalizedUserName = "USER",
                            PasswordHash = "AQAAAAIAAYagAAAAEDX2o9eCOH3cIY9KIvqDj6F+FtO3XRnHAuyTUOu53E4A3njtRzlQ1VhqnNsmSKoChA==",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("98a1f1a7-e250-473a-8765-49ca43260d6f"),
                            ConcurrencyStamp = "98A1F1A7-E250-473A-8765-49CA43260D6F",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("261ef0dc-4c66-4c6d-9d92-0aca4888ed13"),
                            ConcurrencyStamp = "261EF0DC-4C66-4C6D-9D92-0ACA4888ED13",
                            Name = "Moderator",
                            NormalizedName = "MODERATOR"
                        },
                        new
                        {
                            Id = new Guid("8ff0dacb-d7db-4286-8361-bf4f49c13802"),
                            ConcurrencyStamp = "8FF0DACB-D7DB-4286-8361-BF4F49C13802",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba"),
                            RoleId = new Guid("98a1f1a7-e250-473a-8765-49ca43260d6f")
                        },
                        new
                        {
                            UserId = new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b"),
                            RoleId = new Guid("261ef0dc-4c66-4c6d-9d92-0aca4888ed13")
                        },
                        new
                        {
                            UserId = new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726"),
                            RoleId = new Guid("8ff0dacb-d7db-4286-8361-bf4f49c13802")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<Guid>("FollowersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FollowingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FollowersId", "FollowingId");

                    b.HasIndex("FollowingId");

                    b.ToTable("UserUser");
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.Article", b =>
                {
                    b.HasOne("GreenLibrary.Data.Entities.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GreenLibrary.Data.Entities.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.ArticleLike", b =>
                {
                    b.HasOne("GreenLibrary.Data.Entities.Article", "Article")
                        .WithMany("ArticleLikes")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GreenLibrary.Data.Entities.User", "User")
                        .WithMany("LikedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.Tag", b =>
                {
                    b.HasOne("GreenLibrary.Data.Entities.Article", "Article")
                        .WithMany("Tags")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("GreenLibrary.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("GreenLibrary.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenLibrary.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("GreenLibrary.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("GreenLibrary.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("FollowersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenLibrary.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.Article", b =>
                {
                    b.Navigation("ArticleLikes");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("GreenLibrary.Data.Entities.User", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("LikedArticles");
                });
#pragma warning restore 612, 618
        }
    }
}
