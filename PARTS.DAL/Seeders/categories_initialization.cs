using PARTS.DAL.Entities.Item;

namespace PARTS.DAL.Seeders
{
    public static class CategoryData
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Title = "Internal equipment",
                    ParentId = null,
                    Description = "Внутрішнє обладнання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("88ed0184-e8af-4d9a-8d86-a7457aebf3a4"),
                    Title = "Trunk/body",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Багажник/кузов",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fceef809-e5b6-4ca7-a0b2-2953ff9f7976"),
                    Title = "Accessories",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Приладдя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("85a39191-bed7-40ba-9de6-2998dec50f0e"),
                    Title = "Sheathing",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Обшивка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("455ed15f-4fb7-4b78-80b6-bff31c75791c"),
                    Title = "Dashboard",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Приладова панель",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d863b4c5-b143-451d-b139-03ad40f4a7b1"),
                    Title = "Window Lifter",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Склопідйомник",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6769dcfe-bc40-4843-b705-5713d5e94588"),
                    Title = "Manual/pedal mechanism",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Ручний/педальний механізм",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("220ccd19-e744-4ab4-adea-5754edd5f96e"),
                    Title = "Pens",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Ручки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("25ec8ee2-1fce-4eb9-b1e7-0901d18ffd22"),
                    Title = "Seats",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Сидіння",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("838b200c-8075-49f0-9940-f7f3b1d83ed5"),
                    Title = "Gas springs",
                    ParentId = Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00"),
                    Description = "Газові пружини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("12fdbf9d-f2ca-41f8-8c87-6928bfdadc0d"),
                    Title = "Main gear",
                    ParentId = null,
                    Description = "Головна передача",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f144a7ad-845f-438f-88e7-275eb7806ac5"),
                    Title = "Differential",
                    ParentId = Guid.Parse("12fdbf9d-f2ca-41f8-8c87-6928bfdadc0d"),
                    Description = "Диференціал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bd2bf2da-ee00-413d-8e06-1966b3f717d8"),
                    Title = "oil",
                    ParentId = Guid.Parse("12fdbf9d-f2ca-41f8-8c87-6928bfdadc0d"),
                    Description = "Оливи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("096999e2-fb81-4898-a4d9-c92d6070c17e"),
                    Title = "Cardan shaft",
                    ParentId = Guid.Parse("12fdbf9d-f2ca-41f8-8c87-6928bfdadc0d"),
                    Description = "Карданний вал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2c084035-bc85-489d-9efc-94316653c39c"),
                    Title = "Hinge/disc",
                    ParentId = Guid.Parse("096999e2-fb81-4898-a4d9-c92d6070c17e"),
                    Description = "Шарнір/диск",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("673fa4b1-607f-4e76-affb-16df47eb6461"),
                    Title = "Parts for service /maintenance/ care",
                    ParentId = null,
                    Description = "Деталі для сервісу / то / уходу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("133ae2c0-7a45-489d-8f4f-13ebeee4c6a8"),
                    Title = "ADDITIONAL WORK",
                    ParentId = Guid.Parse("673fa4b1-607f-4e76-affb-16df47eb6461"),
                    Description = "Додаткові роботи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a4d4459f-3832-4871-ad60-c25853235790"),
                    Title = "Adjustment interval",
                    ParentId = Guid.Parse("673fa4b1-607f-4e76-affb-16df47eb6461"),
                    Description = "Інтервал регулювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Title = "Comfort systems",
                    ParentId = null,
                    Description = "Системи комфорту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("24241228-01d0-4f54-8b58-28d941b6aebc"),
                    Title = "Autonomous heating system",
                    ParentId = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Description = "Система автономного опалення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ae0e812b-000b-4f0f-9fb6-1e379bd41d1f"),
                    Title = "Motor/relays/switches",
                    ParentId = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Description = "Двигун/реле/перемикачі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("797b3f18-4aac-4622-b100-88d9dbba4d76"),
                    Title = "Mirror",
                    ParentId = Guid.Parse("ae0e812b-000b-4f0f-9fb6-1e379bd41d1f"),
                    Description = "Дзеркало",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ed8f0c00-5d46-480f-a968-80b995205178"),
                    Title = "Speed control system",
                    ParentId = Guid.Parse("ae0e812b-000b-4f0f-9fb6-1e379bd41d1f"),
                    Description = "Система регулювання швидкості",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ae8eca18-9a10-4b1d-8d3e-58a1fa4afc3b"),
                    Title = "Window Lifter",
                    ParentId = Guid.Parse("ae0e812b-000b-4f0f-9fb6-1e379bd41d1f"),
                    Description = "Склопідйомник",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e3b5ee87-f9d1-48e7-8644-361fc136a9dd"),
                    Title = "Pumps",
                    ParentId = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Description = "Насоси",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6b7ca653-2ecb-4361-a985-54c26dcd5ae0"),
                    Title = "Window Lifter",
                    ParentId = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Description = "Склопідйомник",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("eb73cf0d-5ed9-4f74-a60e-3fef24917c62"),
                    Title = "Parking sensor/reversing light",
                    ParentId = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Description = "Паркувальний датчик/сигналізатор заднього ходу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9612000e-2a15-4f5a-a0ba-fcf9f07dcc71"),
                    Title = "Speed control system",
                    ParentId = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Description = "Система регулювання швидкості",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bf49663d-44c0-4ec7-9048-e101025f75e5"),
                    Title = "Central locking",
                    ParentId = Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca"),
                    Description = "Центральний замок",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b"),
                    Title = "Lock system",
                    ParentId = null,
                    Description = "Система замків",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("98e2fac7-30bc-46ad-9fd0-23e0124930fa"),
                    Title = "Locks outside",
                    ParentId = Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b"),
                    Description = "Замки зовні",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("01c913d3-7d2a-48a6-ae0d-578805fc917f"),
                    Title = "Interior locks",
                    ParentId = Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b"),
                    Description = "Замки у салоні",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bb616d54-dfa0-4e61-8d81-42c578887b86"),
                    Title = "Pens",
                    ParentId = Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b"),
                    Description = "Ручки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e53bc8ed-ac1d-4cec-ba1b-782dcb2379be"),
                    Title = "Details",
                    ParentId = Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b"),
                    Description = "Деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("084907b7-6481-418b-9c79-da226a12a542"),
                    Title = "Central locking",
                    ParentId = Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b"),
                    Description = "Центральний замок",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f47198b0-6a1d-4103-93b1-2327d0570585"),
                    Title = "Lock cylinder/set",
                    ParentId = Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b"),
                    Description = "Циліндр замка/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00c0efc7-4c40-42b1-9ebc-b02bea62e161"),
                    Title = "Information/communication systems",
                    ParentId = null,
                    Description = "Інформаційні/комунікаційні системи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f86e7d67-166d-4a33-af4f-72f78a51a27a"),
                    Title = "Antenna",
                    ParentId = Guid.Parse("00c0efc7-4c40-42b1-9ebc-b02bea62e161"),
                    Description = "Антена",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("69d40b9b-890c-4d8d-bc41-0c1c1dcf80fc"),
                    Title = "Audio system",
                    ParentId = Guid.Parse("00c0efc7-4c40-42b1-9ebc-b02bea62e161"),
                    Description = "Аудіосистема",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9dd40059-c3b6-4113-aa34-7aa41a00cc70"),
                    Title = "Chain",
                    ParentId = Guid.Parse("00c0efc7-4c40-42b1-9ebc-b02bea62e161"),
                    Description = "Зв’язок",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ef816943-e43a-4ac2-9d58-95b0f67d7554"),
                    Title = "Wheels/tires",
                    ParentId = null,
                    Description = "Колеса/шини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("094c63ad-291d-4ab7-bfed-424f9f4d5cfc"),
                    Title = "Accessories",
                    ParentId = Guid.Parse("ef816943-e43a-4ac2-9d58-95b0f67d7554"),
                    Description = "Приладдя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Title = "air conditioning",
                    ParentId = null,
                    Description = "Система кондиціонування повітря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2e01571b-ecf3-4b50-86cc-ba539a65ec5a"),
                    Title = "Switch",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Перемикач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("187e4a5a-856f-47d4-a5cc-89bacea479b4"),
                    Title = "Sensors",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Датчики",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b3808537-81c1-48f6-b7f0-f127aa54c594"),
                    Title = "Evaporator",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Випарник",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("dcea0e93-3dae-4588-a8bd-b50f6f6bc0cf"),
                    Title = "Valves",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Клапани",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("538af3df-044a-4e56-b27c-901347834326"),
                    Title = "Compressor/parts",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Компресор/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4ed6968e-f62b-4fbf-ae0c-237ee64e5362"),
                    Title = "Capacitor",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Конденсатор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("23cdf0ed-8f15-4eff-8404-2b677fa389ea"),
                    Title = "Desiccant",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Осушувач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9c83c29e-6e65-4111-82cc-2c89d5978c69"),
                    Title = "Repair Kit",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Ремонтний комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("118db76e-fa64-49d4-bc7e-c8d47f2353b9"),
                    Title = "Operation/Regulation",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Експлуатація/регулювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2186fd4c-ba71-4e44-8b9c-da514b7d45a2"),
                    Title = "Hoses/Piping",
                    ParentId = Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2"),
                    Description = "Шланги/трубопровід",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("99f47e60-8abb-4c83-b5f6-b9c20b34fe62"),
                    Title = "Gearbox",
                    ParentId = null,
                    Description = "Коробка передач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9ff89900-8cc4-4787-a592-e636a095155b"),
                    Title = "Automatic transmission",
                    ParentId = Guid.Parse("99f47e60-8abb-4c83-b5f6-b9c20b34fe62"),
                    Description = "Автоматична коробка передач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("cc22c0b7-4a14-4fa3-bce1-2ad7afcb0564"),
                    Title = "Gearbox",
                    ParentId = Guid.Parse("9ff89900-8cc4-4787-a592-e636a095155b"),
                    Description = "Коробка передач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bf5913e9-efa8-4617-98b7-63aa2cdc0353"),
                    Title = "Oil sump/attachments",
                    ParentId = Guid.Parse("9ff89900-8cc4-4787-a592-e636a095155b"),
                    Description = "Оливний піддон/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2d7cfb1e-c0e7-4b16-84d3-a27889ec1a1f"),
                    Title = "Oil sump",
                    ParentId = Guid.Parse("bf5913e9-efa8-4617-98b7-63aa2cdc0353"),
                    Description = "Оливний піддон",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("77c939cf-2518-496c-ad30-fdabb97a2473"),
                    Title = "Bearing support",
                    ParentId = Guid.Parse("9ff89900-8cc4-4787-a592-e636a095155b"),
                    Description = "Підшипникова опора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e8f5e40b-55ae-480a-b638-1abcb5857b2e"),
                    Title = "Rope drive",
                    ParentId = Guid.Parse("9ff89900-8cc4-4787-a592-e636a095155b"),
                    Description = "Тросовий привод",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d6517747-3b40-45ec-a2b7-4c54a5ec2f9a"),
                    Title = "Control/hydraulic system",
                    ParentId = Guid.Parse("9ff89900-8cc4-4787-a592-e636a095155b"),
                    Description = "Керування/гідравлічна система",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bf130138-e57f-404e-afd2-8748f12cf94a"),
                    Title = "Search products by image",
                    ParentId = Guid.Parse("99f47e60-8abb-4c83-b5f6-b9c20b34fe62"),
                    Description = "Пошук товарів за зображенням",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9f982650-9664-4e10-aea0-e8bc6cc08196"),
                    Title = "Gearbox",
                    ParentId = Guid.Parse("99f47e60-8abb-4c83-b5f6-b9c20b34fe62"),
                    Description = "Коробка передач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c5efe932-fd10-47c5-b5f0-2a7ae613a706"),
                    Title = "Sensor",
                    ParentId = Guid.Parse("9f982650-9664-4e10-aea0-e8bc6cc08196"),
                    Description = "Датчик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9a8e511e-79c9-4544-a14e-06a981177dc9"),
                    Title = "Bearing support",
                    ParentId = Guid.Parse("9f982650-9664-4e10-aea0-e8bc6cc08196"),
                    Description = "Підшипникова опора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5e62e60f-3697-4b8e-865b-170040a09578"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("9f982650-9664-4e10-aea0-e8bc6cc08196"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7dfd0df0-315b-4ebc-919b-f8a9b3d7ac86"),
                    Title = "Gearbox drive mechanism",
                    ParentId = Guid.Parse("9f982650-9664-4e10-aea0-e8bc6cc08196"),
                    Description = "Приводний механізм коробки передач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Title = "Heating/Ventilation System",
                    ParentId = null,
                    Description = "Система обігріву/вентиляції",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b54814af-1485-46bb-963a-3677663a8c37"),
                    Title = "Control units",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Блоки керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("80d1c3d2-125b-4648-b207-8aae34df938e"),
                    Title = "Blower Fan Parts",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Деталі вентилятора-нагнітача",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5217ee08-d927-4386-9b4f-fa8d2bb0124c"),
                    Title = "Valves/adjustments",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Клапани/регулювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("58983cc9-5e25-4caa-8337-fa46437048d4"),
                    Title = "Cooling water heating",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Підігрів охолоджувальної води",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("009280f3-7a91-4d05-a84b-03c0ea8d567d"),
                    Title = "Interior Heat Exchanger",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Теплообмінник салону",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2d85c996-a476-4a0e-a66c-481b26758366"),
                    Title = "Cabin air filter",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Повітряний фільтр салону",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d2996e87-18f9-4c89-b837-7f45cca572cc"),
                    Title = "Hoses/Pipes",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Шланги/труби",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c6c5b2ce-b070-41ca-a21d-30a6a9840dfb"),
                    Title = "Controls ",
                    ParentId = Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87"),
                    Description = "Елементи керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("71b79f78-0e98-47fb-a8d7-39f49be6bb80"),
                    Title = "Pneumatic installation",
                    ParentId = null,
                    Description = "Пневматична установка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("219a2aab-bd01-46be-aebb-68032405a7f2"),
                    Title = "Valves/pneumatic installation",
                    ParentId = Guid.Parse("71b79f78-0e98-47fb-a8d7-39f49be6bb80"),
                    Description = "Клапани/пневматична установка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b6a898e9-5e37-44a3-a59d-547030a4a286"),
                    Title = "Other valves",
                    ParentId = Guid.Parse("219a2aab-bd01-46be-aebb-68032405a7f2"),
                    Description = "Інші клапани",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e"),
                    Title = "Suspension/ Depreciation",
                    ParentId = null,
                    Description = "Підвіска/ амортизація",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("74a41599-1ab1-4e4f-b69d-bf1a6c589c61"),
                    Title = "Shock absorber",
                    ParentId = Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e"),
                    Description = "Амортизатор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ec465085-f395-49bb-b86e-954b15361009"),
                    Title = "Air suspension",
                    ParentId = Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e"),
                    Description = "Пневматична підвіска",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("cd0dcc10-8810-475c-b4eb-3e7839e389df"),
                    Title = "Spring suspension",
                    ParentId = Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e"),
                    Description = "Пружинна підвіска",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("84a1aa12-3267-4821-baa0-123dc24e5192"),
                    Title = "Shock strut/shock absorber support",
                    ParentId = Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e"),
                    Description = "Опора амортизаційної стійки/амортизатора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d62ad812-c7ed-486a-97bc-a02f27b7ca48"),
                    Title = "Level Control System/Chassis Hydraulic System",
                    ParentId = Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e"),
                    Description = "Система регулювання рівня/гідравл. система ходової частини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e674620b-e9a2-4bd8-930b-96d610776880"),
                    Title = "Shock rack/shock absorber/hinged components",
                    ParentId = Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e"),
                    Description = "Амортизаційна стійка/амортизатор/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2fad5ffa-4d83-436e-80e0-e7c56a08c9c4"),
                    Title = "Hinged components",
                    ParentId = Guid.Parse("e674620b-e9a2-4bd8-930b-96d610776880"),
                    Description = "Навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0f2d173e-0cf3-447e-af00-76bef4a545cf"),
                    Title = "Shock absorption rack",
                    ParentId = Guid.Parse("e674620b-e9a2-4bd8-930b-96d610776880"),
                    Description = "Амортизаційна стійка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Title = "Axle Suspension/Wheel Suspension Guide System/Wheels",
                    ParentId = null,
                    Description = "Підвіска осі/напрямна система підвіски коліс/колеса",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("76441a56-2b00-4c58-bc58-b00c65d6943d"),
                    Title = "Axle beam/axle body/support",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Балка осі/тіло осі/опора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4f8d3c98-4368-4265-84e9-50b0d190b554"),
                    Title = "Axle beam/subframe",
                    ParentId = Guid.Parse("76441a56-2b00-4c58-bc58-b00c65d6943d"),
                    Description = "Балка осі/підрамник",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("73265098-2e6c-4297-8f22-5427ddd8f7ff"),
                    Title = "Bearing support",
                    ParentId = Guid.Parse("76441a56-2b00-4c58-bc58-b00c65d6943d"),
                    Description = "Підшипникова опора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fd187043-9d6c-480a-87b4-ecced0613b34"),
                    Title = "Tools",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Інструменти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6d293103-d2fc-4d05-ac91-d4a780eb762b"),
                    Title = "Wheel/Wheel Mount",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Колесо/кріплення колеса",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4e56ee62-0b16-4792-a1f6-c7908f944aa6"),
                    Title = "Axle Neck/Repair Kit",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Шийка осі/ремонтний комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8853839f-fce1-4fb4-b18d-4398f33a0b8d"),
                    Title = "Axle Neck",
                    ParentId = Guid.Parse("4e56ee62-0b16-4792-a1f6-c7908f944aa6"),
                    Description = "Шийка осі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("83779681-1948-4b3d-be3c-b37d6be0b68f"),
                    Title = "Shock rack mounting/support",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Кріплення/опора амортизаційної стійки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d39782d1-c111-44bf-b0c8-43838c0f2453"),
                    Title = "Guide rocker/guide rocker support",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Напрямне коромисло/опора напрямного коромисла",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0b6acdb7-6312-452d-99ec-a0fd33d0dabc"),
                    Title = "Support/fixture",
                    ParentId = Guid.Parse("d39782d1-c111-44bf-b0c8-43838c0f2453"),
                    Description = "Опора/кріплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3fca533f-731a-4d27-9ca9-ae2fcde1336a"),
                    Title = "Voltage of the rocker arm (front, late, diagonal. suspension lever)",
                    ParentId = Guid.Parse("d39782d1-c111-44bf-b0c8-43838c0f2453"),
                    Description = "Напр. коромисло (попер., позд., діагонал. важіль підвіски)",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("63b6b363-60e3-4cab-98b7-baeab4d3d233"),
                    Title = "Expansion of the wheelbase",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Розширення колісної бази",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6184d8e6-986d-405f-9575-4cfd010fc4d1"),
                    Title = "Stabilizer/fasteners",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Стабілізатор/кріпильні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("feba373d-fb2a-41eb-bd81-107e5807cced"),
                    Title = "Fasteners",
                    ParentId = Guid.Parse("6184d8e6-986d-405f-9575-4cfd010fc4d1"),
                    Description = "Кріпильні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("54e66c6d-fdd9-498f-8878-2f30a78acf6c"),
                    Title = "Tie rod",
                    ParentId = Guid.Parse("6184d8e6-986d-405f-9575-4cfd010fc4d1"),
                    Description = "З’єднувальна тяга",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e409ee78-b9d6-4e00-abf0-b57c3e37ada4"),
                    Title = "Fins ",
                    ParentId = Guid.Parse("6184d8e6-986d-405f-9575-4cfd010fc4d1"),
                    Description = "Стабілізатор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("50409b12-4e17-436e-a71f-727153e8db6d"),
                    Title = "Shaky support",
                    ParentId = Guid.Parse("6184d8e6-986d-405f-9575-4cfd010fc4d1"),
                    Description = "Хитна опора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9acb67f7-5065-4275-9332-18ae035ac1cf"),
                    Title = "Retaining bars/rods",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Підпірні бруси/штанги",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("cfb80d6c-3a08-41ae-9b90-dbce9fa20f9e"),
                    Title = "Hub/wheel support",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Маточина/опора колеса",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("aa6dfef2-0e84-4754-867d-295db5f80d9d"),
                    Title = "Support, wheel bearing housing",
                    ParentId = Guid.Parse("cfb80d6c-3a08-41ae-9b90-dbce9fa20f9e"),
                    Description = "Опора, корпус підшипника колеса",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8a0582bd-180c-449f-9fc9-80cccff937af"),
                    Title = "Wheel bearing",
                    ParentId = Guid.Parse("cfb80d6c-3a08-41ae-9b90-dbce9fa20f9e"),
                    Description = "Підшипник колеса",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a8e5be91-2b13-40b1-8cd5-8ab930156608"),
                    Title = "Wheel hub",
                    ParentId = Guid.Parse("cfb80d6c-3a08-41ae-9b90-dbce9fa20f9e"),
                    Description = "Маточина колеса",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ac8deec1-afe5-40d1-a418-f456dcc1c7c0"),
                    Title = "Axle trunnion",
                    ParentId = Guid.Parse("cfb80d6c-3a08-41ae-9b90-dbce9fa20f9e"),
                    Description = "Цапфа осі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8a733676-ceb7-4187-8e7e-abd96b963258"),
                    Title = "Hinges",
                    ParentId = Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e"),
                    Description = "Шарніри",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6f90ad3f-4d25-4c9b-b6d5-17f690018e85"),
                    Title = "Lower/upper ball joint",
                    ParentId = Guid.Parse("8a733676-ceb7-4187-8e7e-abd96b963258"),
                    Description = "Нижній/верхній кульовий шарнір",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4156a36e-b261-49c1-ab25-28a50eb40c6d"),
                    Title = "Fuel preparation",
                    ParentId = null,
                    Description = "Підготовка палива",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ac6fecfb-ef1f-4009-bcfb-56f45d26618d"),
                    Title = "Self Cleaning",
                    ParentId = Guid.Parse("4156a36e-b261-49c1-ab25-28a50eb40c6d"),
                    Description = "Система очищення ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ab58ef48-fb11-4caf-ba47-36a5998728f0"),
                    Title = "Lambda Regulation",
                    ParentId = Guid.Parse("ac6fecfb-ef1f-4009-bcfb-56f45d26618d"),
                    Description = "Лямбда-регулювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a0e069e6-5472-48ce-b7d1-1c2c08ee986e"),
                    Title = "HV recirculation system",
                    ParentId = Guid.Parse("ac6fecfb-ef1f-4009-bcfb-56f45d26618d"),
                    Description = "Система рециркуляції ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c5c3cdbe-c011-400c-8d93-c81fb8d87672"),
                    Title = "HV recirculation valve/inlet pipe",
                    ParentId = Guid.Parse("a0e069e6-5472-48ce-b7d1-1c2c08ee986e"),
                    Description = "Клапан/впускна труба системи рециркуляції ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a457bae7-77c2-4bad-ae8b-305e94990f1b"),
                    Title = "Valves, HV recirculation system",
                    ParentId = Guid.Parse("a0e069e6-5472-48ce-b7d1-1c2c08ee986e"),
                    Description = "Клапани, система рециркуляції ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("229bc139-496e-4afc-b46c-5b051d538c4a"),
                    Title = "Pressure converter",
                    ParentId = Guid.Parse("a0e069e6-5472-48ce-b7d1-1c2c08ee986e"),
                    Description = "Перетворювач тиску",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ca1648f2-6c90-4cd1-94a6-728508e06975"),
                    Title = "HV recirculation control system",
                    ParentId = Guid.Parse("a0e069e6-5472-48ce-b7d1-1c2c08ee986e"),
                    Description = "Система керування рециркуляцією ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Title = "Mixture preparation system",
                    ParentId = Guid.Parse("4156a36e-b261-49c1-ab25-28a50eb40c6d"),
                    Description = "Система приготування суміші",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("04e35c25-9af3-47f8-8b8d-c0686cf5f43d"),
                    Title = "Control unit",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Блок керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("92ecf094-4c02-48c6-acb7-4d13c7ae8921"),
                    Title = "Switch/relay",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Перемикач/реле",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1438d047-3346-44fc-b06e-5a0f9a9cc067"),
                    Title = "Sensor/probe",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Датчик/зонд",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b1fbe06c-f121-4616-a7b2-80e19e9f00f6"),
                    Title = "Air flow meter",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Витратомір повітря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3cc91701-cad2-41ea-b097-ebd291e9526b"),
                    Title = "Tools",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Інструменти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("72ce0140-183c-4b4a-a0e4-4f78080d9016"),
                    Title = "Injection valve/injector nozzle/emulsion tube/BNF",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Клапан впорск./інжекторна форсунка/емульсійна трубка/БНФ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b5b27bfa-0055-4c59-ba18-3dbcd7fd7c33"),
                    Title = "Valves/Valve block",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Клапани/блок клапанів",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("658da0ec-702d-4bd2-8203-e1c8dcf22ac7"),
                    Title = "Fuel Injection Pump/High Pressure Pump",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Насос впорскування палива/насос високого тиску",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("204f2d0c-bf54-4067-93a0-bdd28ff7f735"),
                    Title = "Pedal/Gas",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Педаль ходу/газу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a5882b43-6919-4f9b-b436-de93c436fc20"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a244c49d-c2b6-438c-aec4-de2780d56c69"),
                    Title = "Repair/complete set",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Ремонтний/повний комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("168da60b-3d7c-4c20-a8f8-ae4b98bd40fd"),
                    Title = "Emulsion tube/nozzle parts",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Деталі емульсійної трубки/форсунки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2e44a274-bf0d-4ae9-9cd5-5259707fde98"),
                    Title = "Stopping mechanism",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Механізм зупинки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("51160178-5372-44e6-8799-1de3f2618e79"),
                    Title = "Fuel line/fuel distribution/dosing system",
                    ParentId = Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841"),
                    Description = "Паливопровід/система розподілу/дозування палива",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b05d3151-70cf-417e-a9ba-d393d8a28ddc"),
                    Title = "Carburetor system",
                    ParentId = Guid.Parse("4156a36e-b261-49c1-ab25-28a50eb40c6d"),
                    Description = "Карбюраторна система",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0aede8ee-2f3c-4ae6-9e52-6b7dd6470bf1"),
                    Title = "Carburetor parts",
                    ParentId = Guid.Parse("b05d3151-70cf-417e-a9ba-d393d8a28ddc"),
                    Description = "Деталі карбюратора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8ebe5047-e7a7-4003-83af-9abb08fb7b41"),
                    Title = "Wheel drive",
                    ParentId = null,
                    Description = "Привод коліс",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("10763d32-ec2e-40f6-b4e2-098995c4a6ca"),
                    Title = "Hinge/set",
                    ParentId = Guid.Parse("8ebe5047-e7a7-4003-83af-9abb08fb7b41"),
                    Description = "Шарнір/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9fbcefd2-29a8-436f-a30f-c93d7c31dbfa"),
                    Title = "Fasteners/accessories",
                    ParentId = Guid.Parse("8ebe5047-e7a7-4003-83af-9abb08fb7b41"),
                    Description = "Кріпильні компоненти/приладдя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("beacce80-e39a-4c39-ad4c-b7ac2d0e5884"),
                    Title = "Drive shaft",
                    ParentId = Guid.Parse("8ebe5047-e7a7-4003-83af-9abb08fb7b41"),
                    Description = "Приводний вал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bcf75878-e4c5-4a1c-98ed-100e71715a53"),
                    Title = "Corrugated casing",
                    ParentId = Guid.Parse("8ebe5047-e7a7-4003-83af-9abb08fb7b41"),
                    Description = "Гофрований кожух",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d24e0f97-bdd1-4f2b-a951-0d4cc371731d"),
                    Title = "Hitch/attachments",
                    ParentId = null,
                    Description = "Зчіпний пристрій/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("dd54db99-fba0-4fe1-80c4-5fa7c6fed548"),
                    Title = "Coupling device",
                    ParentId = Guid.Parse("d24e0f97-bdd1-4f2b-a951-0d4cc371731d"),
                    Description = "Зчіпний пристрій",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24"),
                    Title = "Belt drive",
                    ParentId = null,
                    Description = "Ремінний привод",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("60ea012e-5c35-46f9-adc7-2f02ddb247aa"),
                    Title = "V-belt/set",
                    ParentId = Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24"),
                    Description = "Клиновий ремінь/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("707237fb-b179-4bc3-ad6f-562fdada791c"),
                    Title = "V-belt",
                    ParentId = Guid.Parse("60ea012e-5c35-46f9-adc7-2f02ddb247aa"),
                    Description = "Клиновий ремінь",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("03da70d2-1199-4dc1-b889-5546be965097"),
                    Title = "Generator freewheeling mechanism",
                    ParentId = Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24"),
                    Description = "Механізм вільного ходу генератора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Title = "Poly V-belt/kit",
                    ParentId = Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24"),
                    Description = "Поліклиновий ремінь/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("004ec141-c431-4881-b682-c08cd3fdf3f7"),
                    Title = "Dampener",
                    ParentId = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Description = "Демпфер",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5086c386-2467-4664-ac5b-b91e465957d8"),
                    Title = "Poly V Belts Set",
                    ParentId = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Description = "Комплект поліклинових ременів",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4459c0df-8859-45df-a082-82e1197e8f62"),
                    Title = "Belt tensioning device (tensioning unit)",
                    ParentId = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Description = "Натяжний пристрій (натяжний блок) ременя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5f30c1e9-5056-4026-a372-d27848c1fce2"),
                    Title = "tensioning bar",
                    ParentId = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Description = "Натяжна планка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("700aa6eb-b40a-41e1-bfa1-0966c089b4d3"),
                    Title = "Retractor/guide roller",
                    ParentId = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Description = "Відвідний/напрямний ролик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("396effab-5f65-4730-aea6-6ad59bf2fc38"),
                    Title = "Poly V-belt",
                    ParentId = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Description = "Поліклиновий ремінь",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("16a9e81c-5f6b-4287-bfe4-97cac1f32393"),
                    Title = "Tension roller",
                    ParentId = Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533"),
                    Description = "Натяжний ролик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("103aaba1-1e21-49f9-aa2b-17931dee4f65"),
                    Title = "Accessories/small parts",
                    ParentId = Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24"),
                    Description = "Приладдя/дрібні частини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b6849a86-43c4-4a77-9fa7-e2713d236c8b"),
                    Title = "Belt pulley",
                    ParentId = Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24"),
                    Description = "Шків ременя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7dafbdf0-6fe1-410d-82d9-f2ee57e052c2"),
                    Title = "Toothed belt/kit",
                    ParentId = Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24"),
                    Description = "Зубчастий ремінь/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1238a4e2-deac-4be0-8581-ec1fe01d3a93"),
                    Title = "Retractor/guide roller",
                    ParentId = Guid.Parse("7dafbdf0-6fe1-410d-82d9-f2ee57e052c2"),
                    Description = "Відвідний/напрямний ролик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("25cb2d4b-5287-4a9f-8614-c7714a23df6f"),
                    Title = "Tension roller",
                    ParentId = Guid.Parse("7dafbdf0-6fe1-410d-82d9-f2ee57e052c2"),
                    Description = "Натяжний ролик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Title = "Steering mechanism",
                    ParentId = null,
                    Description = "Механізм рульового керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("12f4e593-2367-4715-acaf-82a61cd0ab90"),
                    Title = "Corrugated casing/seal",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Гофрований кожух/ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f150521b-4ac5-405e-8fbe-0fa7c788a520"),
                    Title = "Steering column/shaft",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Рульова колонка/вал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("af417b59-5ab4-4103-92e1-d377e397f502"),
                    Title = "oil",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Оливи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5c4155f2-d390-4fec-8d78-81ce7c38160d"),
                    Title = "Steering gear transmission components",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Передавальні компоненти механізму рульового керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2b7b578f-0219-4002-999b-c9255f9c3241"),
                    Title = "Search products by image",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Пошук товарів за зображенням",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d212b984-6604-4ef1-b64b-398c00fb6716"),
                    Title = "Tie Rod Ends/Parts",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Поперечні рульові тяги/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fba76d3d-16a3-4a42-bdf8-e4fdf381b006"),
                    Title = "Transverse Tie Rod Ends Details",
                    ParentId = Guid.Parse("d212b984-6604-4ef1-b64b-398c00fb6716"),
                    Description = "Деталі поперечних рульових тяг",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d18b32ec-cd3e-41fd-b805-e0342c47307e"),
                    Title = "Repair Kit",
                    ParentId = Guid.Parse("d212b984-6604-4ef1-b64b-398c00fb6716"),
                    Description = "Ремонтний комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2d01a147-7cfb-4431-a4f6-4643c82cb0f2"),
                    Title = "Tie Rod Ends",
                    ParentId = Guid.Parse("d212b984-6604-4ef1-b64b-398c00fb6716"),
                    Description = "Поперечні рульові тяги",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("82b196a2-fb8b-423c-b92c-ecc3f782098d"),
                    Title = "Steering gear/pump",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Рульовий механізм/насос",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("16d7f788-25e5-474d-b12a-ee9a5edc5ded"),
                    Title = "Steering mechanism filter",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Фільтр механізму рульового керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("20118ac8-98be-4c2c-924b-fc029a18d091"),
                    Title = "Hinges",
                    ParentId = Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33"),
                    Description = "Шарніри",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0f2e2c52-ca98-4961-9ea9-3b01c639ece6"),
                    Title = "System Safety",
                    ParentId = null,
                    Description = "Системи безпеки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2980adcf-d54b-41c0-85ae-b7b93408bfb7"),
                    Title = "Alarm system",
                    ParentId = Guid.Parse("0f2e2c52-ca98-4961-9ea9-3b01c639ece6"),
                    Description = "Система сигналізації",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7107471e-187b-4461-9220-abcd350e28ce"),
                    Title = "Airbag system",
                    ParentId = Guid.Parse("0f2e2c52-ca98-4961-9ea9-3b01c639ece6"),
                    Description = "Система подушок безпеки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Title = "AH drainage system",
                    ParentId = null,
                    Description = "Система відведення ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("78d8a9a8-de24-4e78-9021-d57cb6975764"),
                    Title = "Silencer",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Глушник",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("25b5f7e2-8bd3-420b-986e-aa560e9f0e78"),
                    Title = "Silencer assembly",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Глушник в зборі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("36cd8d37-a526-469c-bbbf-5cd4b9f10d39"),
                    Title = "Sensor/probe",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Датчик/зонд",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ad0d142b-ab2a-4b20-8837-990138f713cc"),
                    Title = "Mounting components",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Монтажні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Title = "Mounting parts",
                    ParentId = Guid.Parse("ad0d142b-ab2a-4b20-8837-990138f713cc"),
                    Description = "Деталі для монтажу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c97336d2-135c-4cca-91a6-3b1c7bc53501"),
                    Title = "Screw/Nut/Rings",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Гвинт/гайка/кільця",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ab88ee67-5bef-45ef-9ce6-ccd1a4b222ae"),
                    Title = "Welding coupling",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Зварювальна муфта",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6f8bbe23-b3d3-4803-8a40-f9539bd1ee38"),
                    Title = "Clamping element",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Затискний елемент",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("699a2268-6efb-4f6c-b9de-306b26b50e87"),
                    Title = "Bracket",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Кронштейн",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("07d9f546-3712-4005-81cd-68dfbb0d5ac7"),
                    Title = "Rubber buffer",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Гумовий буфер",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5921bc93-df17-4e36-8c4e-26a053880f1c"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("704366db-55bb-45b6-8cb3-a2580d4f8ed8"),
                    Title = "Rubber ring",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Гумове кільце",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bd5cec11-1a9f-46bb-839d-3a343db2ae34"),
                    Title = "Rubber band",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Гумова стрічка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("19a9e700-b0cd-4e70-af08-1eac30e84955"),
                    Title = "Shackle",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Скоба",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3c7065f0-72f0-4c8a-ae41-03ccfeb595f9"),
                    Title = "Clamp",
                    ParentId = Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5"),
                    Description = "Хомут",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b14e64c2-558e-439b-b14d-2f71c16bd91f"),
                    Title = "Mounting kit",
                    ParentId = Guid.Parse("ad0d142b-ab2a-4b20-8837-990138f713cc"),
                    Description = "Комплект для монтажу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("cb86681f-7c64-44e1-bd1d-f1276078f036"),
                    Title = "Catalyst",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Каталізатор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5c4c6d09-6738-47b2-ab76-a417870974ed"),
                    Title = "Exhaust manifold",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Випускний колектор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("03bb03b0-6355-41cd-aa01-ccbce8da2a3d"),
                    Title = "Oxygen sensor",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Кисневий датчик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5f64cb6b-a0fc-4e18-844a-39072526ca5e"),
                    Title = "Supercharger",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Нагнітач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a1dbec9e-80a4-4277-b83a-be57e5552d5b"),
                    Title = "Anti-shrink/dust filter",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Протисажний/пиловий фільтр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0ea54717-8799-484f-ac3f-b7614385bd94"),
                    Title = "Conduits",
                    ParentId = Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce"),
                    Description = "Труби",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a149b62c-548e-44e1-9e96-f312d9b08577"),
                    Title = "Ignition/Inflammation System",
                    ParentId = null,
                    Description = "Система запалювання/розжарювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e0eecc85-b117-456c-813a-4f25add9a887"),
                    Title = "Control unit/relays/sensors",
                    ParentId = Guid.Parse("a149b62c-548e-44e1-9e96-f312d9b08577"),
                    Description = "Блок керування/реле/датчики",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e27ba410-9b8c-4c5a-a1c8-1d29c6fe92e6"),
                    Title = "Pulse sensor",
                    ParentId = Guid.Parse("a149b62c-548e-44e1-9e96-f312d9b08577"),
                    Description = "Імпульсний датчик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("248c2932-2034-43e1-9d66-c66cd2ad9f6f"),
                    Title = "Incandescent candle",
                    ParentId = Guid.Parse("a149b62c-548e-44e1-9e96-f312d9b08577"),
                    Description = "Свічка розжарювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Title = "COOLING SYSTEM",
                    ParentId = null,
                    Description = "Система охолодження",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("751056a9-4261-411e-aa05-6d5bb8336823"),
                    Title = "Full coolant",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Антифриз",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("37b90a0a-30aa-4e4a-abad-2efa714286c4"),
                    Title = "Fan",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Вентилятор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9b787865-af44-45d3-92c4-1d5d9d5af88b"),
                    Title = "Water/oil radiator",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Водяний/оливний радіатор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e05db368-4d62-43cd-a075-62f9b80cf91b"),
                    Title = "Water radiator/parts",
                    ParentId = Guid.Parse("9b787865-af44-45d3-92c4-1d5d9d5af88b"),
                    Description = "Водяний радіатор/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b5ef1cc0-5cc2-4d56-9294-fffbb10a0b3a"),
                    Title = "Oil cooler",
                    ParentId = Guid.Parse("9b787865-af44-45d3-92c4-1d5d9d5af88b"),
                    Description = "Оливний радіатор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1d21477f-6146-4a4b-92d0-5a72d62e9187"),
                    Title = "Interior Heat Exchanger",
                    ParentId = Guid.Parse("9b787865-af44-45d3-92c4-1d5d9d5af88b"),
                    Description = "Теплообмінник салону",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("105c1050-1b30-4f18-a825-29ce176d9ade"),
                    Title = "Overflow bottle",
                    ParentId = Guid.Parse("9b787865-af44-45d3-92c4-1d5d9d5af88b"),
                    Description = "Розширювальний бачок",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a0c82443-a95e-4e82-bd5c-c56b14c21a32"),
                    Title = "Water pumps/seals",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Водяні насоси/ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("42601cc7-3f34-4b4b-9248-815da984c8e7"),
                    Title = "the water pump",
                    ParentId = Guid.Parse("a0c82443-a95e-4e82-bd5c-c56b14c21a32"),
                    Description = "Водяний насос",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("656754df-ebd0-42a5-812d-8c7009fefbdd"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("a0c82443-a95e-4e82-bd5c-c56b14c21a32"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("908769ce-d05c-4d4f-bed2-b9c321854b9d"),
                    Title = "Switch/Sensor",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Перемикач/датчик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2f14b5cd-ef03-4c99-835d-a8e47ad3cf4d"),
                    Title = "Relays",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Реле",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9907384a-90b3-4559-b7d9-f977865bd461"),
                    Title = "Air Cooled",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Повітряне охолодження",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7b19c99f-d03b-4e3c-94d6-f09eb871c1b2"),
                    Title = "Hoses/Piping/Flanges",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Шланги/трубопроводи/фланці",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("30be9f41-1fd0-451f-bea0-0efc07f6e6cd"),
                    Title = "Coolant hoses/piping",
                    ParentId = Guid.Parse("7b19c99f-d03b-4e3c-94d6-f09eb871c1b2"),
                    Description = "Шланги/трубопроводи охолоджувальної рідини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("21b777c4-b0cd-49a6-9f0b-f06f370ef357"),
                    Title = "Flanges",
                    ParentId = Guid.Parse("7b19c99f-d03b-4e3c-94d6-f09eb871c1b2"),
                    Description = "Фланці",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("800f7002-4131-4cf3-be72-e7b11362d41c"),
                    Title = "Thermostat/seal",
                    ParentId = Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174"),
                    Description = "Термостат/ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b67b0398-389a-490b-865b-1876936b4849"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("800f7002-4131-4cf3-be72-e7b11362d41c"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0d1a15f8-1b8c-4b08-9920-54a7e3a658ef"),
                    Title = "Thermostat ",
                    ParentId = Guid.Parse("800f7002-4131-4cf3-be72-e7b11362d41c"),
                    Description = "Термостат",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Title = "Wiper system",
                    ParentId = null,
                    Description = "Система склоочисників",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0ad1d1bc-a1a0-459e-97fd-feffea06a259"),
                    Title = "Wash water tank/piping",
                    ParentId = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Description = "Бак води для миття/трубопровід",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("556e018b-400a-463e-8e34-193742893c3a"),
                    Title = "Wash water pump",
                    ParentId = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Description = "Насос подачі води для миття",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("dbd271e4-e5bf-4512-93be-c9fb6bc36b65"),
                    Title = "Switch/relay",
                    ParentId = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Description = "Перемикач/реле",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c6ac633e-ed3a-40e5-bfa2-be47ba005c62"),
                    Title = "Wiper motor",
                    ParentId = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Description = "Двигун склоочисника",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9a55fb09-d8b3-4a9f-879b-718e35a26f38"),
                    Title = "Wash water nozzle",
                    ParentId = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Description = "Форсунка подачі води для миття",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("97902e64-5cdd-42da-bd62-e0e051d5bbff"),
                    Title = "Wiper blade/rubber band",
                    ParentId = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Description = "Щітка/гумова стрічка склоочисника",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("eed6fa47-21c5-467b-a671-c44cc361314b"),
                    Title = "Wiper rods/actuator",
                    ParentId = Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093"),
                    Description = "Тяги/привод склоочисника",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("13120958-20c0-450f-b1a1-e60633a0a25d"),
                    Title = "Self Cleaning",
                    ParentId = null,
                    Description = "Система очищення фар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8ff9369c-e8fb-4883-984a-f6770edf2dc4"),
                    Title = "Fuel supply system",
                    ParentId = null,
                    Description = "Система подачі палива",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("cffcbb2e-10c5-4a66-9583-eb82f1719737"),
                    Title = "Pump/Accessories",
                    ParentId = Guid.Parse("8ff9369c-e8fb-4883-984a-f6770edf2dc4"),
                    Description = "Насос/приладдя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f0886e10-ea44-4777-ba49-cd450fefe44b"),
                    Title = "Fuel pump",
                    ParentId = Guid.Parse("cffcbb2e-10c5-4a66-9583-eb82f1719737"),
                    Description = "Паливний насос",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9ae9dbf8-3128-4604-aa2e-8b84a14e0cd4"),
                    Title = "Hoses/Piping",
                    ParentId = Guid.Parse("8ff9369c-e8fb-4883-984a-f6770edf2dc4"),
                    Description = "Шланги/трубопроводи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("48970c6b-3730-43ee-9de8-4f52d66b3133"),
                    Title = "Fuel tank/accessories",
                    ParentId = Guid.Parse("8ff9369c-e8fb-4883-984a-f6770edf2dc4"),
                    Description = "Паливний бак/приладдя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a6ee258e-6961-4a8d-bdd2-b31f2813d42b"),
                    Title = "Fuel filter/housing",
                    ParentId = Guid.Parse("8ff9369c-e8fb-4883-984a-f6770edf2dc4"),
                    Description = "Паливний фільтр/корпус",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("37c1a316-8dba-41b8-89f5-d3ff03ce870e"),
                    Title = "Feeding Assembly Assembly",
                    ParentId = Guid.Parse("8ff9369c-e8fb-4883-984a-f6770edf2dc4"),
                    Description = "Вузол подачі в зборі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77"),
                    Title = "Clutch/attachments",
                    ParentId = null,
                    Description = "Зчеплення/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("caf279b5-5ae5-4d1c-a6e2-c281eb2327a9"),
                    Title = "Clutch disc",
                    ParentId = Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77"),
                    Description = "Диск зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e55f8e47-5411-4095-89de-63c3007fbdca"),
                    Title = "Clutch Kit",
                    ParentId = Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77"),
                    Description = "Комплект зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("81c38ec8-177a-4291-b198-0075b1cb06b2"),
                    Title = "Disc flywheel",
                    ParentId = Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77"),
                    Description = "Дисковий маховик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9e79b62c-607b-4eaf-8ea6-3f0f09e1a337"),
                    Title = "Clutch pressure patch",
                    ParentId = Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77"),
                    Description = "Натискний диск зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("572ccaee-c5ce-439c-bc5b-536b9f01740b"),
                    Title = "Bearing/Clutch Disengagement Center Clutch",
                    ParentId = Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77"),
                    Description = "Підшипник/центральна муфта вимкнення зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a61ead94-4a61-4902-9735-923250bcfc55"),
                    Title = "Clutch Disengagement Bearing",
                    ParentId = Guid.Parse("572ccaee-c5ce-439c-bc5b-536b9f01740b"),
                    Description = "Підшипник вимкнення зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c11b8863-82ca-43aa-b699-3f9df1938cb4"),
                    Title = "Central clutch disengagement clutch",
                    ParentId = Guid.Parse("572ccaee-c5ce-439c-bc5b-536b9f01740b"),
                    Description = "Центральна муфта вимкнення зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2c0b86fc-67ab-4573-a51e-f686ff4b2334"),
                    Title = "Clutch drive",
                    ParentId = Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77"),
                    Description = "Привод зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("518fed00-f314-4475-9f24-4ea51e5260a6"),
                    Title = "Main cylinder",
                    ParentId = Guid.Parse("2c0b86fc-67ab-4573-a51e-f686ff4b2334"),
                    Description = "Головний циліндр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("dfe10ae9-a428-4f21-9a6d-4a9d880fac92"),
                    Title = "Pedal",
                    ParentId = Guid.Parse("2c0b86fc-67ab-4573-a51e-f686ff4b2334"),
                    Description = "Педаль",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("30035eb8-a2a7-4022-8050-506cc759f63b"),
                    Title = "Actuating cylinder",
                    ParentId = Guid.Parse("2c0b86fc-67ab-4573-a51e-f686ff4b2334"),
                    Description = "Виконавчий циліндр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("822969f0-2b65-44bf-8779-470af03c518b"),
                    Title = "Hoses/Piping",
                    ParentId = Guid.Parse("2c0b86fc-67ab-4573-a51e-f686ff4b2334"),
                    Description = "Шланги/трубопроводи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("468fe5f5-effc-42ea-a4f4-76e9e96b476a"),
                    Title = "Clutch cable",
                    ParentId = Guid.Parse("2c0b86fc-67ab-4573-a51e-f686ff4b2334"),
                    Description = "Тросик зчеплення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Title = "Brake Assembly",
                    ParentId = null,
                    Description = "Гальмівна установка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8"),
                    Title = "Drum Brake",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Барабанне гальмо",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("23a6825f-aa0a-4879-890a-583d7690bf6a"),
                    Title = "Wheel cylinder",
                    ParentId = Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8"),
                    Description = "Колісний циліндр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f854e9f6-d212-44d9-82be-b03f2261da90"),
                    Title = "Accessories/parts",
                    ParentId = Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8"),
                    Description = "Приладдя/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("264c79f8-d6e7-43bd-83c9-b484ef6719ce"),
                    Title = "Repair Kit",
                    ParentId = Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8"),
                    Description = "Ремонтний комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7f3f92d5-800e-4204-b82d-70556343d71f"),
                    Title = "parking brake",
                    ParentId = Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8"),
                    Description = "стоянковим гальмом",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1ff2fb84-f563-4bcf-814e-8a7c5c4b45ea"),
                    Title = "Brake Pad/Shoe",
                    ParentId = Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8"),
                    Description = "Гальмівна накладка/колодка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fde57802-5a0a-4a4d-8bc1-2f71a839553e"),
                    Title = "Brake drum",
                    ParentId = Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8"),
                    Description = "Гальмівний барабан",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("faba878d-b6d3-4f00-b9b1-bd46df55fe33"),
                    Title = "Brake light switch",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Перемикач стоп-сигналу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0da13f48-91af-4e57-94be-32280b3be9aa"),
                    Title = "Brake Master Cylinder",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Головний гальмівний циліндр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("60d2b89f-01ca-4af6-b1e5-60888a727b57"),
                    Title = "Wear Indicator",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Індикатор зносу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("751baa67-0e3d-45fb-bb38-3830a86fff7d"),
                    Title = "Disc Brake",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Дискове гальмо",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7e73e60d-d0da-4d51-8100-9de296e523dc"),
                    Title = "Brake Kit",
                    ParentId = Guid.Parse("751baa67-0e3d-45fb-bb38-3830a86fff7d"),
                    Description = "Комплект гальма",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f9ec579a-6598-49f5-8d1a-2b5eb4c1f6ae"),
                    Title = "Accessories/parts",
                    ParentId = Guid.Parse("751baa67-0e3d-45fb-bb38-3830a86fff7d"),
                    Description = "Приладдя/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a43a59c8-386d-4353-b05a-fdcfa3e228bd"),
                    Title = "Brake disc",
                    ParentId = Guid.Parse("751baa67-0e3d-45fb-bb38-3830a86fff7d"),
                    Description = "Гальмівний диск",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a33124fb-cedf-45ce-9878-38d54b6c0b15"),
                    Title = "Brake Pad",
                    ParentId = Guid.Parse("751baa67-0e3d-45fb-bb38-3830a86fff7d"),
                    Description = "Гальмівна накладка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4ab5fac4-bb96-4de8-a4be-37cccc3bcd79"),
                    Title = "Wheel cylinder",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Колісний циліндр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("27ed2474-031e-468a-8085-3b0dd79aac7d"),
                    Title = "Motion dynamics control system",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Система регулювання динаміки руху",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("72022f4e-31b2-47d0-b568-175d01d4cb0c"),
                    Title = "Brake force regulator",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Регулятор гальмівного зусилля",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e5cf7bd6-c997-460a-9666-9cebe3fa5dd0"),
                    Title = "Levers/ropes/tie rods",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Важелі/троси/тяги",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("72ed4f46-e843-4562-a2cc-5c123980bea2"),
                    Title = "parking brake",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "стоянковим гальмом",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d8d6661f-059b-45c2-86fa-da79ba78423e"),
                    Title = "Brake caliper",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Гальмівний супорт",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a3bb2099-5708-486e-b6fe-1b293039d8c4"),
                    Title = "Details",
                    ParentId = Guid.Parse("d8d6661f-059b-45c2-86fa-da79ba78423e"),
                    Description = "Деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8cd60338-df94-4bc3-8ec8-3e9b76b8c3d5"),
                    Title = "Brake caliper/bracket (holder)",
                    ParentId = Guid.Parse("d8d6661f-059b-45c2-86fa-da79ba78423e"),
                    Description = "Гальмівний супорт/кронштейн (тримач)",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e0d68c4d-4e32-44b4-bee2-7617f085b0f6"),
                    Title = "Brake fluid",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Гальмівна рідина",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("99cc7c27-dbf8-4c09-89d7-11ce0f8dbc32"),
                    Title = "Brake pipelines",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Гальмівні трубопроводи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a5e07096-6a73-411a-b309-6856220ca9fa"),
                    Title = "Brake hoses",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Гальмівні шланги",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ab8a287c-b6bb-423f-9bd7-2dfcc2dfcde8"),
                    Title = "Brake force booster",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Підсилювач гальмівного зусилля",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3c79e720-342a-472d-b963-920f1be94042"),
                    Title = "Filter",
                    ParentId = Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6"),
                    Description = "Фільтр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Title = "Electricity",
                    ParentId = null,
                    Description = "Електрика",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b0b515ad-4e48-4920-ba1a-24dc1af39628"),
                    Title = "Battery",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Акумулятор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c9137508-997c-44f1-83b1-8017e8764121"),
                    Title = "Switches/relays/lighting controllers",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Перемикачі/реле/контролери освітлення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b6023dfc-bd62-45ee-b2e4-a7c45d01362a"),
                    Title = "Switch/Regulator",
                    ParentId = Guid.Parse("c9137508-997c-44f1-83b1-8017e8764121"),
                    Description = "Перемикач/регулятор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d58ed966-a5ef-4a02-9902-97f1db959699"),
                    Title = "Relays",
                    ParentId = Guid.Parse("c9137508-997c-44f1-83b1-8017e8764121"),
                    Description = "Реле",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2a398538-71de-40a3-8593-0d51d2e6a9da"),
                    Title = "Generator/Parts",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Генератор/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8dda310d-56b4-4657-a4c5-756fde48a51b"),
                    Title = "Generator",
                    ParentId = Guid.Parse("2a398538-71de-40a3-8593-0d51d2e6a9da"),
                    Description = "Генератор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("90f01348-5275-4131-a95e-34a9bc483a47"),
                    Title = "Regulatory authority",
                    ParentId = Guid.Parse("2a398538-71de-40a3-8593-0d51d2e6a9da"),
                    Description = "Регулятор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c95cee84-57ce-4b04-af19-29420aa6c73a"),
                    Title = "Details",
                    ParentId = Guid.Parse("2a398538-71de-40a3-8593-0d51d2e6a9da"),
                    Description = "Деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c024b14e-3c46-4c30-b3e6-dcb1d8600d77"),
                    Title = "Sensors",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Датчики",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ea99f683-f0a6-44c2-b45c-c016286c4662"),
                    Title = "Auxiliary headlight/parts",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Додаткова фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4a308364-baa2-4d16-8ce7-eee2201c71c3"),
                    Title = "Fog lamp/parts",
                    ParentId = Guid.Parse("ea99f683-f0a6-44c2-b45c-c016286c4662"),
                    Description = "Протитуманна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("66a8eb15-e080-4a46-896a-358d754392d8"),
                    Title = "Fog lamp/optical element",
                    ParentId = Guid.Parse("4a308364-baa2-4d16-8ce7-eee2201c71c3"),
                    Description = "Протитуманна фара/оптичний елемент",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7e0e8a36-bebd-4572-8d7d-6ea7fc3bd817"),
                    Title = "Fog lamp incandescent lamp",
                    ParentId = Guid.Parse("4a308364-baa2-4d16-8ce7-eee2201c71c3"),
                    Description = "Лампа розжарювання протитуманної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7e8f04bd-ede0-4297-8746-41c59786115b"),
                    Title = "Headlamp/Parts",
                    ParentId = Guid.Parse("ea99f683-f0a6-44c2-b45c-c016286c4662"),
                    Description = "Основна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e2a510c0-164b-4051-b13f-ae61763851f2"),
                    Title = "High beam headlamp incandescent lamp",
                    ParentId = Guid.Parse("7e8f04bd-ede0-4297-8746-41c59786115b"),
                    Description = "Лампа розжарювання фари дальнього світла",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("09ea07b6-0b18-4392-84fb-d60c682b3984"),
                    Title = "Headlamp with rotary light/parts",
                    ParentId = Guid.Parse("ea99f683-f0a6-44c2-b45c-c016286c4662"),
                    Description = "Фара з поворотним світлом/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5d2b0a77-c268-42e6-be7e-767a8720d28c"),
                    Title = "Horn/beep",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Гудок/звуковий сигнал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("67468c21-f260-48a0-9b8b-ad1cc9f6e678"),
                    Title = "Cable set/individual cable components",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Комплект кабелів/окремі компоненти кабелю",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c9e80e8a-2bb2-4f8c-9074-1c1881428e6f"),
                    Title = "devices,",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Прилади",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c05b7693-c9c1-483f-b50b-3c148e83d8a1"),
                    Title = "Sensors/switches (relays)",
                    ParentId = Guid.Parse("c9e80e8a-2bb2-4f8c-9074-1c1881428e6f"),
                    Description = "Датчики/перемикачі (реле)",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("35fc035b-a493-4a71-b4c0-5f7357d5359b"),
                    Title = "Multifunctional relay",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Багатофункціональне реле",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d6cecb15-9faf-488e-9a26-1e327d2d46f7"),
                    Title = "Headlamp/Parts",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Основна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("cf10f55b-5571-4b33-9d43-207bed508975"),
                    Title = "Main headlamp incandescent lamp",
                    ParentId = Guid.Parse("d6cecb15-9faf-488e-9a26-1e327d2d46f7"),
                    Description = "Лампа розжарювання основної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6444d20a-1450-42f6-9022-cda95c91c310"),
                    Title = "Main headlight/optical element",
                    ParentId = Guid.Parse("d6cecb15-9faf-488e-9a26-1e327d2d46f7"),
                    Description = "Основна фара/оптичний елемент",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1d197273-f749-4612-a2d8-d474ecdb6099"),
                    Title = "Main headlight parts",
                    ParentId = Guid.Parse("d6cecb15-9faf-488e-9a26-1e327d2d46f7"),
                    Description = "Деталі основної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ecaa4718-f2d5-4266-9e1a-0b3a789705b2"),
                    Title = "Adjusting the light range",
                    ParentId = Guid.Parse("d6cecb15-9faf-488e-9a26-1e327d2d46f7"),
                    Description = "Регулювання дальності світла",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("79e67d6d-a16a-4bf0-899e-49799e9af055"),
                    Title = "Direction indicator/relay breaker",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Переривач вказівників повороту/реле",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fdb016c9-d458-4691-abd6-b18211470ee9"),
                    Title = "Control units",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Блоки керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("67e25b9e-b875-4fda-8bf4-12921898d24c"),
                    Title = "Relays",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Реле",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Title = "Lights",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Ліхтарі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117"),
                    Title = "Interior lighting",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Освітлення салону",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5dffee01-f846-4901-bf86-977fe3be8b62"),
                    Title = "Reading luminaire",
                    ParentId = Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117"),
                    Description = "Світильник для читання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("97dc45bd-8bce-4293-9d88-595e01efbd6d"),
                    Title = "Trunk/body lighting",
                    ParentId = Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117"),
                    Description = "Освітлення багажника/кузова",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("631eee68-94a5-4d87-b78e-da321155741f"),
                    Title = "Illumination of the engine compartment",
                    ParentId = Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117"),
                    Description = "Освітлення підкапотного простору",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ccf7ec04-363c-42d4-ba27-df340a595893"),
                    Title = "Lighting of devices/controls",
                    ParentId = Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117"),
                    Description = "Освітлення приладів/органів керування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f4b94717-03d6-4c55-90da-4f88d3c2d4e2"),
                    Title = "Glove compartment lighting",
                    ParentId = Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117"),
                    Description = "Освітлення бардачка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ac3d4f87-2312-4e0d-9725-cd7688562e83"),
                    Title = "Interior lighting",
                    ParentId = Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117"),
                    Description = "Освітлення салону",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("96e47e82-d6ba-4223-b615-4be02b5be94c"),
                    Title = "Running light",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Ходовий ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d32e1b0d-8a6b-411f-a46f-7a7b996ea2f6"),
                    Title = "Tail Light/Parts",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Задній ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8c04ca8a-fe07-434d-95c8-ba53f6f22b70"),
                    Title = "Tail light",
                    ParentId = Guid.Parse("d32e1b0d-8a6b-411f-a46f-7a7b996ea2f6"),
                    Description = "Задній ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ffb1ed1a-d2fa-4166-bcd9-a8a8d442326d"),
                    Title = "Tail light incandescent lamp",
                    ParentId = Guid.Parse("d32e1b0d-8a6b-411f-a46f-7a7b996ea2f6"),
                    Description = "Лампа розжарювання заднього ліхтаря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("342686dd-5784-450f-9951-31ff408f3885"),
                    Title = "Rear fog lamp/parts",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Задня протитуманна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2ede01b4-9956-4a29-b465-39754287294c"),
                    Title = "Rear fog lamp incandescent lamp",
                    ParentId = Guid.Parse("342686dd-5784-450f-9951-31ff408f3885"),
                    Description = "Лампа розжарювання задньої протитуманної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ce0c13c1-056b-414d-b7cf-aed9e7bfa2b2"),
                    Title = "Side-/side-marker lamp/parts",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Боковий-/габаритний ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6e0faf33-074f-483c-83ad-59950c256458"),
                    Title = "Clearance lamp",
                    ParentId = Guid.Parse("ce0c13c1-056b-414d-b7cf-aed9e7bfa2b2"),
                    Description = "Габаритний ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1802b2db-df73-42de-8379-473aae5d596e"),
                    Title = "Tungsten",
                    ParentId = Guid.Parse("ce0c13c1-056b-414d-b7cf-aed9e7bfa2b2"),
                    Description = "Лампа розжарювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8f0591e3-bf76-48db-a5b6-20de16fb4205"),
                    Title = "Parking/parking light",
                    ParentId = Guid.Parse("ce0c13c1-056b-414d-b7cf-aed9e7bfa2b2"),
                    Description = "Паркувальний/стоянковий ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e0ad65d7-419e-4cc9-9af2-75e9479f28b0"),
                    Title = "Reversing light/parts",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Ліхтар заднього ходу/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("47fd9365-bf20-4d8e-8b07-f6fe29e474f1"),
                    Title = "Reversing lamp incandescent lamp",
                    ParentId = Guid.Parse("e0ad65d7-419e-4cc9-9af2-75e9479f28b0"),
                    Description = "Лампа розжарювання ліхтаря заднього ходу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ac572b78-6498-4c86-9ccd-2d17bc2c4f90"),
                    Title = "License Plate Lantern/Parts",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Ліхтар номерного знака/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("aacd346d-7205-4272-8dfe-1c9b93bb25b7"),
                    Title = "License plate lamp incandescent lamp",
                    ParentId = Guid.Parse("ac572b78-6498-4c86-9ccd-2d17bc2c4f90"),
                    Description = "Лампа розжарювання ліхтаря номерного знака",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8f5d5679-40fd-4f73-84ef-fc7c88abc9a8"),
                    Title = "Brake Light/Parts",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Стоп-сигнал/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("88e6bdfb-1018-48ed-928d-eb177989bceb"),
                    Title = "Auxiliary brake light",
                    ParentId = Guid.Parse("8f5d5679-40fd-4f73-84ef-fc7c88abc9a8"),
                    Description = "Додатковий стоп-сигнал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("24df10c4-365c-4076-a96f-3fba1862fea1"),
                    Title = "Brake light incandescent lamp",
                    ParentId = Guid.Parse("8f5d5679-40fd-4f73-84ef-fc7c88abc9a8"),
                    Description = "Лампа розжарювання стоп-сигналу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f844f426-c6db-4d88-84aa-ac0111683890"),
                    Title = "Turn Signal/Parts",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Вказівник повороту/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b78c14ef-a218-4093-baf3-1bfb194024db"),
                    Title = "Turn Signal Incandescent Lamp",
                    ParentId = Guid.Parse("f844f426-c6db-4d88-84aa-ac0111683890"),
                    Description = "Лампа розжарювання вказівника повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e97f8921-b6d2-479e-ba9d-27e0b4030dab"),
                    Title = "Turn Signal",
                    ParentId = Guid.Parse("f844f426-c6db-4d88-84aa-ac0111683890"),
                    Description = "Вказівник повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("98e6b75b-f526-49d1-b62f-4c32147f730c"),
                    Title = "Lantern at the door",
                    ParentId = Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6"),
                    Description = "Ліхтар у дверях",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("84ae7906-9d02-47a6-aaf6-7a50a1392005"),
                    Title = "Start-up system",
                    ParentId = Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712"),
                    Description = "Система пуску",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("19a25fbf-641c-4944-81fc-64f7a1ff8209"),
                    Title = "Details",
                    ParentId = Guid.Parse("84ae7906-9d02-47a6-aaf6-7a50a1392005"),
                    Description = "Деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d8e15269-e6ad-450b-ae28-754e38d61cb1"),
                    Title = "Starter",
                    ParentId = Guid.Parse("84ae7906-9d02-47a6-aaf6-7a50a1392005"),
                    Description = "Стартер",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("90f81574-e863-4a88-9a42-7f0641981a55"),
                    Title = "Magnetic switch",
                    ParentId = Guid.Parse("84ae7906-9d02-47a6-aaf6-7a50a1392005"),
                    Description = "Магнітний перемикач",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Title = "Posterior",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Задня частина ТЗ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Title = "Anterior",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Передня частина ТЗ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e59b0edb-84a0-452e-bb43-f05898dd8249"),
                    Title = "Luggage/cargo compartment/cargo platform",
                    ParentId = Guid.Parse("c716307a-76b6-4feb-a31d-c2c3691a885d"),
                    Description = "Багажне/вантажне відділення/вантажна платформа",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b55e1df7-ebef-4d7c-a580-89af2475d8cb"),
                    Title = "Crank case",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Картер",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2b770362-38c5-4b2a-8671-e6a1c90d2388"),
                    Title = "Crank case",
                    ParentId = Guid.Parse("b55e1df7-ebef-4d7c-a580-89af2475d8cb"),
                    Description = "Картер",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7aa43e11-b0d3-4a8f-b92b-8b3665d9e3db"),
                    Title = "Side wall",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Бокова стінка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("67cc9768-0cf5-4b4b-b6f6-3e868c546162"),
                    Title = "Side wall",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Бокова стінка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("612c1495-9a7e-491a-bc42-493f1a81339f"),
                    Title = "Side wall",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Бокова стінка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("08db2e69-2c2c-400f-b2f4-7e281956b190"),
                    Title = "Side turn lights",
                    ParentId = Guid.Parse("02c71b5a-a91d-4b81-a29d-4c8eb7b1c0fb"),
                    Description = "Бокові вказівники повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("dbd289b2-f333-45e5-94fa-e9964f0dbaec"),
                    Title = "Connecting rod screw/nut",
                    ParentId = Guid.Parse("aaaf11ef-70f0-4d5e-8b25-9f38c9aae554"),
                    Description = "Гвинт/гайка шатуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("182338f3-eb50-4986-ba94-0a42a01bc5b4"),
                    Title = "Cylinder head screws",
                    ParentId = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Description = "Гвинти голівки циліндра",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a85a437f-715c-473d-870c-e9b290feb4ff"),
                    Title = "Bumper/parts",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Бампер/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("13ccf5d1-0362-490c-8c6e-0852da2ad9e6"),
                    Title = "Bumper/parts",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Бампер/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7e397d12-c68d-4ac7-b235-343447d781f7"),
                    Title = "Connecting rod bearing",
                    ParentId = Guid.Parse("aaaf11ef-70f0-4d5e-8b25-9f38c9aae554"),
                    Description = "Підшипник шатуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c1ff46b4-c012-459d-9d4c-3000af501383"),
                    Title = "Crankshaft bearing",
                    ParentId = Guid.Parse("d859a756-cb15-46ae-8aa6-587bfd8ff6e7"),
                    Description = "Підшипник колінчатого вала",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("20047c4d-f70c-4ce0-9d2d-12fb364d0474"),
                    Title = "Bonded slabs",
                    ParentId = Guid.Parse("3429252b-950a-4700-9168-4ea58cabdb56"),
                    Description = "Вклеєні шибки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("216df717-d107-4e5c-9ba4-58952e91533c"),
                    Title = "Air filter",
                    ParentId = Guid.Parse("8862fdb4-f8ac-433c-8edb-d3bd1306abbf"),
                    Description = "Повітряний фільтр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b7292803-8f94-4ecc-98dd-58b4ea6fd59c"),
                    Title = "Air filter/air filter casing",
                    ParentId = Guid.Parse("72f3a4b3-c989-4276-9d3d-9ca40a025ee8"),
                    Description = "Повітряний фільтр/кожух повітряного фільтра",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6abecb7a-4bbe-4e28-9b43-34a8df3681e9"),
                    Title = "Inlet manifold/inlet pipe",
                    ParentId = Guid.Parse("72f3a4b3-c989-4276-9d3d-9ca40a025ee8"),
                    Description = "Впускний колектор/впускна труба",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2bab86fd-bc19-4cd1-85fd-24d243426a31"),
                    Title = "Subframe/subframe",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Надрамник/підрамник",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6ee7fdbb-262a-4eaa-bffc-eadc58f6a4f4"),
                    Title = "Connecting rod lower head bushings",
                    ParentId = Guid.Parse("aaaf11ef-70f0-4d5e-8b25-9f38c9aae554"),
                    Description = "Втулки нижньої голівки шатуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4aaf5d56-2e3c-4072-93f5-68f2a90af6d8"),
                    Title = "Clearance lamp",
                    ParentId = Guid.Parse("4001dc55-58f1-4082-be35-835a4dd23337"),
                    Description = "Габаритний ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ca453283-67c0-405a-8d92-a93e1d2fcf8a"),
                    Title = "Clearance lamp",
                    ParentId = Guid.Parse("d686c65e-d13d-4445-84a0-e5aaec5c80d9"),
                    Description = "Габаритний ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1a08dcc5-4760-44cb-b861-f11f33b31f3f"),
                    Title = "Clearance lamp",
                    ParentId = Guid.Parse("84868942-43e3-497e-99c2-208146b33433"),
                    Description = "Габаритний ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("39b348d7-c668-48b1-a783-b73afe728a80"),
                    Title = "Tail Light/Parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Задній ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b01c7796-0bbf-4a1a-b8bd-2f39a2de8c4e"),
                    Title = "Tail Light/Parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Задній ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("feca2ad8-302a-4402-aa21-63505709f97b"),
                    Title = "Gas springs",
                    ParentId = Guid.Parse("d90bbbdd-2e2b-43f2-9119-41e5d34d670e"),
                    Description = "Газові пружини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ad166207-f418-4dc3-bb00-11ff6bbcb435"),
                    Title = "Gas springs",
                    ParentId = Guid.Parse("51e3b6e4-9870-4170-bfd0-770a003edc82"),
                    Description = "Газові пружини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("72c138ce-1c8d-4c12-b231-ff5060d879a3"),
                    Title = "Gas springs",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Газові пружини",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bd34ca56-2bb2-4c62-8c14-7a8d787943e5"),
                    Title = "Oil circuit sealing",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Герметизація оливного контуру",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b8094280-b417-4d69-935f-0aaf19eb1275"),
                    Title = "Sealing of the cooling water supply system",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Герметизація системи подачі охолоджувальної води",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fb07cfa9-af7f-461f-baa8-3918104702a3"),
                    Title = "Sealing of fuel supply system",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення системи подачі палива",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3c5c9e2f-1e34-4d22-b682-4ab19d3370a8"),
                    Title = "Cylinder operating bushings/kit",
                    ParentId = Guid.Parse("b55e1df7-ebef-4d7c-a580-89af2475d8cb"),
                    Description = "Робочі втулки циліндра/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f7b8ba20-a20f-4622-807b-f68548798ba5"),
                    Title = "Distribution Box/Seal",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Картер розподільного механізму/ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("729b5f86-a05e-4b08-a193-eb068e5a6f0f"),
                    Title = "Cylindrical head",
                    ParentId = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Description = "Циліндрична голівка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Title = "Cylinder head/attachments",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Голівка циліндра/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d2efb523-d640-4c5a-aa65-8848e80ae0ef"),
                    Title = "Hydraulic switch/sensor/valve",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Гідравлічний вимикач/датчик/клапан",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("48e350b7-dbf7-49a1-a59c-6df718b079f8"),
                    Title = "Door/Parts",
                    ParentId = Guid.Parse("9b0782a0-895b-42f1-8ac2-f46ab8e83dac"),
                    Description = "Двері/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f728dc70-4201-423c-9fca-ca1e56af87f8"),
                    Title = "Door/Parts",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Двері/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("54b6c928-e45b-4982-ac6e-f6cacc439259"),
                    Title = "Door/side glass",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Дверне/бокове скло",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d90bbbdd-2e2b-43f2-9119-41e5d34d670e"),
                    Title = "Spare parts/attachments",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Запасні деталі/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("51e3b6e4-9870-4170-bfd0-770a003edc82"),
                    Title = "Spare parts/attachments",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Запасні деталі/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3979809f-3a4b-4331-b1c1-ca268e687b5c"),
                    Title = "Spare parts/attachments",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Запасні деталі/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Title = "Body parts/fender/bumper",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Компоненти кузова/крило/бампер",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("bdc66ff4-6bbd-4fe3-a4cb-c2e914b0cf47"),
                    Title = "Crankshaft washers",
                    ParentId = Guid.Parse("d859a756-cb15-46ae-8aa6-587bfd8ff6e7"),
                    Description = "Шайби колінчатого вала",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("66ef6f29-b9f4-44c9-9ad8-d7b7f5e2ccfe"),
                    Title = "Auxiliary headlight/parts",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Додаткова фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d8a522ec-8e9d-475a-a13a-68a97570376f"),
                    Title = "Auxiliary brake light",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Додатковий стоп-сигнал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0c5a1faa-c003-4a03-870a-ac0ca2cc76f0"),
                    Title = "Auxiliary brake light",
                    ParentId = Guid.Parse("b280a8f0-53dd-47b6-a369-b9b2e82b0a79"),
                    Description = "Додатковий стоп-сигнал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1f6c2465-26e4-44d9-a136-12808058613f"),
                    Title = "Auxiliary brake light",
                    ParentId = Guid.Parse("5a1a9635-fc82-4af3-9412-d39ead6f9ac4"),
                    Description = "Додатковий стоп-сигнал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4500e132-44d0-46de-a8da-1debd9c120b3"),
                    Title = "Rear window",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Заднє скло",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5ab2bcd9-d459-4245-bb70-cf1bb7981686"),
                    Title = "Tail light",
                    ParentId = Guid.Parse("fe24f92e-cac4-4546-a711-05557ddf13c8"),
                    Description = "Задній ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0130ecdb-b77f-4ecd-86a9-f5f708f660e9"),
                    Title = "Tail light",
                    ParentId = Guid.Parse("948c1715-8bb3-4932-b87b-0a2fa511725d"),
                    Description = "Задній ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("948c1715-8bb3-4932-b87b-0a2fa511725d"),
                    Title = "Tail Light/Parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Задній ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fe24f92e-cac4-4546-a711-05557ddf13c8"),
                    Title = "Tail Light/Parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Задній ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b58ab03b-531c-4562-bba9-a42cba3b6e04"),
                    Title = "Rear Door/Parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Задні двері/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4a670e7e-b891-44ad-aad9-1e0513f04ea7"),
                    Title = "Rear fog lamp/parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Задня протитуманна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("aca1601d-75dd-4108-a77b-631eef70c969"),
                    Title = "Rear fog lamp/parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Задня протитуманна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("43312104-9684-465c-8806-95b0bdc711ae"),
                    Title = "Mirror",
                    ParentId = Guid.Parse("136eb99a-6a6b-4806-9c74-a6391a506bb0"),
                    Description = "Дзеркало",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("71193141-499b-4295-8e83-114233d54ace"),
                    Title = "Mirror",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Дзеркало",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Title = "Passenger compartment",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Пасажирське відділення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7c1b9f7c-0e5a-4c4d-98ee-da9c333b860e"),
                    Title = "Hood/parts/insulation",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Капот/деталі/ізоляція",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5699f1bd-d1d1-4554-a7ec-0f18ca44deab"),
                    Title = "Hood/parts/insulation",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Капот/деталі/ізоляція",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f148c314-a8c8-4204-9f19-c4348a9f5e9c"),
                    Title = "Hood/parts/insulation",
                    ParentId = Guid.Parse("9b0782a0-895b-42f1-8ac2-f46ab8e83dac"),
                    Description = "Капот/деталі/ізоляція",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6d4b92e1-538b-4e52-a63c-c44a753f199d"),
                    Title = "Roof/door racks/frames",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Дах/дверні стійки/рами",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2e3fb39c-588b-44bf-ba6d-efaa613a8134"),
                    Title = "Roof/door racks/frames",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Дах/дверні стійки/рами",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8dcf6465-9e8b-457d-a773-0f24290d5046"),
                    Title = "Valve/Adjustment",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Клапан/регулювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7f93ea54-2958-429e-b7ef-c7d8d4b8c5ec"),
                    Title = "HV recirculation valve/inlet pipe",
                    ParentId = Guid.Parse("00ee2968-d8ba-40e2-ace2-c82ad955025c"),
                    Description = "Клапан/впускна труба системи рециркуляції ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("da48bd19-be0b-4f0b-bf27-a4b97badf4e2"),
                    Title = "Valves/Accessories",
                    ParentId = Guid.Parse("8dcf6465-9e8b-457d-a773-0f24290d5046"),
                    Description = "Клапани/приладдя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f4ec6257-37cd-444a-b93d-a06b156b5182"),
                    Title = "Valves, HV recirculation system",
                    ParentId = Guid.Parse("00ee2968-d8ba-40e2-ace2-c82ad955025c"),
                    Description = "Клапани, система рециркуляції ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("944e35d7-bddb-452a-b621-d3bd50689e70"),
                    Title = "Engine cover.",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Кришка двигуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a20c3baa-4385-4c0c-84bd-1e580bf35852"),
                    Title = "Engine cover.",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Кришка двигуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d6ce2a48-a7dc-4b5c-a156-03316a0e1598"),
                    Title = "Engine cover.",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Кришка двигуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("625a28b8-32ce-47f7-8f66-a13fba56b466"),
                    Title = "Crankshaft",
                    ParentId = Guid.Parse("d859a756-cb15-46ae-8aa6-587bfd8ff6e7"),
                    Description = "Колінчатий вал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d859a756-cb15-46ae-8aa6-587bfd8ff6e7"),
                    Title = "Crankshaft",
                    ParentId = Guid.Parse("9ae1e561-5e61-4644-a880-f396c611d317"),
                    Description = "Колінчатий вал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fe1265f8-2e86-43c7-ab0b-b1e3e193a97c"),
                    Title = "Wheel niche",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Колісна ніша",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("379e1f77-26cc-4027-8baf-d1cfc367daa8"),
                    Title = "Wheel niche",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Колісна ніша",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ab1b95a6-ac1e-430d-b6b8-ae156f118d5b"),
                    Title = "Wheel niche",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Колісна ніша",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7e700b88-4b47-4ec5-a54b-8d9171a8d4b7"),
                    Title = "Piston Ring Kit",
                    ParentId = Guid.Parse("f35e0c6c-f037-46e1-b6d9-3603fa634e44"),
                    Description = "Комплект поршневих кілець",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9e55b858-a1ae-4060-9764-148eef6508bc"),
                    Title = "Complete set of seals",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Повний комплект ущільнень",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6d80e72e-bd28-43bc-81c3-843fac26e5f6"),
                    Title = "Filter set",
                    ParentId = Guid.Parse("8862fdb4-f8ac-433c-8edb-d3bd1306abbf"),
                    Description = "Комплект фільтрів",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("aa6de629-4cf3-443d-926d-5284fbae00c9"),
                    Title = "Drive Chain Kit",
                    ParentId = Guid.Parse("4d4edc30-0ab0-4d7c-a42d-b9cca25cd398"),
                    Description = "Комплект ланцюгів привода",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b5b4b149-fd40-432f-ae33-bbb7b584018d"),
                    Title = "Blower/parts",
                    ParentId = Guid.Parse("5b85feda-23a8-4033-a093-517053bde4ba"),
                    Description = "Нагнітач/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("645d4abd-cb0b-451e-a63c-973ad1d8189b"),
                    Title = "Oil Filter Seal/Housing",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Ущільнення/корпус оливного фільтра",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9ad2bb41-7b88-4818-a480-09e370175a6b"),
                    Title = "Engine Suspension",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Підвіска двигуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("31f9a9e6-16fd-4026-8f40-28bcab32d645"),
                    Title = "Retainer/holder/frame",
                    ParentId = Guid.Parse("3979809f-3a4b-4331-b1c1-ca268e687b5c"),
                    Description = "Фіксатор/тримач/рама",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0e5e7e50-a504-4ff0-8c02-111c0955b8f0"),
                    Title = "Retainer/holder/frame",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Фіксатор/тримач/рама",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9ae1e561-5e61-4644-a880-f396c611d317"),
                    Title = "Crank mechanism",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Кривошипний механізм",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a270bb47-d9c6-4090-9fb6-1c59be69aae7"),
                    Title = "Wing/attachments",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Крило/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e54e1e1d-d3fc-48a6-b1fe-7196e8bad928"),
                    Title = "Wing/attachments",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Крило/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("af3f84f2-2153-491e-8db2-3cd5411b3edb"),
                    Title = "Wing/attachments",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Крило/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a745dcee-a16b-417e-935c-c1920188a13a"),
                    Title = "Cylinder head cover/seal",
                    ParentId = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Description = "Кришка голівки циліндра/ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("db872175-cf00-4079-837e-f99c1b04b336"),
                    Title = "Oil Filler Tube Cover/Seal",
                    ParentId = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Description = "Кришка/ущільнення оливоналивного патрубка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9b0782a0-895b-42f1-8ac2-f46ab8e83dac"),
                    Title = "Covers / enclosures / doors / glass /pan.taxi / folding roof",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Кришки / кожухи / двері / скло / пан.дах / складний дах",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("86f6f8b4-c811-461b-9848-9692792734b9"),
                    Title = "Tungsten",
                    ParentId = Guid.Parse("84868942-43e3-497e-99c2-208146b33433"),
                    Description = "Лампа розжарювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d9139c44-0247-4265-b280-d500080665c8"),
                    Title = "Reversing lamp incandescent lamp",
                    ParentId = Guid.Parse("ce3ceec7-55b8-4dff-a11e-9c19696786e9"),
                    Description = "Лампа розжарювання ліхтаря заднього ходу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4a03bc4a-e53c-4416-b94b-6677dac6ef57"),
                    Title = "License plate lamp incandescent lamp",
                    ParentId = Guid.Parse("47d341cf-3516-4be4-ac23-0bf1b59d5144"),
                    Description = "Лампа розжарювання ліхтаря номерного знака",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1c3f4b3d-3190-4145-b141-daac113666b8"),
                    Title = "Brake light incandescent lamp",
                    ParentId = Guid.Parse("b280a8f0-53dd-47b6-a369-b9b2e82b0a79"),
                    Description = "Лампа розжарювання стоп-сигналу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0804ba66-5ac1-49c5-808e-f3ef156d5151"),
                    Title = "Turn Signal Incandescent Lamp",
                    ParentId = Guid.Parse("02c71b5a-a91d-4b81-a29d-4c8eb7b1c0fb"),
                    Description = "Лампа розжарювання вказівника повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("334b171b-e2c8-409d-bf23-6161041c3f2d"),
                    Title = "Turn Signal Incandescent Lamp",
                    ParentId = Guid.Parse("acfcf892-ce8b-4e07-9fd7-d5712983c396"),
                    Description = "Лампа розжарювання вказівника повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c04fce90-de9f-464f-8ca4-30265105ebcc"),
                    Title = "Tail light incandescent lamp",
                    ParentId = Guid.Parse("39b348d7-c668-48b1-a783-b73afe728a80"),
                    Description = "Лампа розжарювання заднього ліхтаря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("24ff0524-9c1f-450c-bd38-402c5f096d2a"),
                    Title = "Rear fog lamp incandescent lamp",
                    ParentId = Guid.Parse("4a670e7e-b891-44ad-aad9-1e0513f04ea7"),
                    Description = "Лампа розжарювання задньої протитуманної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c8300086-1f8d-45af-8e8a-f4f3e48e1ae7"),
                    Title = "Tungsten",
                    ParentId = Guid.Parse("4001dc55-58f1-4082-be35-835a4dd23337"),
                    Description = "Лампа розжарювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("80d0a20d-da25-4d77-ad20-f9aa772d5eb2"),
                    Title = "Reversing lamp incandescent lamp",
                    ParentId = Guid.Parse("87915875-44d9-490d-9c2d-ed2a1703635e"),
                    Description = "Лампа розжарювання ліхтаря заднього ходу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("53cb0ce9-ef45-4623-87db-3d9e094a0dde"),
                    Title = "License plate lamp incandescent lamp",
                    ParentId = Guid.Parse("182b30ff-077e-486d-af99-05875c6c22b4"),
                    Description = "Лампа розжарювання ліхтаря номерного знака",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4c904767-df8f-4109-ae7f-fe8b9a9c790b"),
                    Title = "Brake light incandescent lamp",
                    ParentId = Guid.Parse("5a1a9635-fc82-4af3-9412-d39ead6f9ac4"),
                    Description = "Лампа розжарювання стоп-сигналу",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("05bbfee8-878e-4972-9e26-58e4b079ecd4"),
                    Title = "Turn Signal Incandescent Lamp",
                    ParentId = Guid.Parse("2b039db9-3b2e-4e44-bb99-bd2317d42442"),
                    Description = "Лампа розжарювання вказівника повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8374dbc4-3b24-4dd3-b24b-9f18275e322d"),
                    Title = "Tail light incandescent lamp",
                    ParentId = Guid.Parse("b01c7796-0bbf-4a1a-b8bd-2f39a2de8c4e"),
                    Description = "Лампа розжарювання заднього ліхтаря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("596e2ac3-c384-4746-8838-d7a1cef2f27f"),
                    Title = "Rear fog lamp incandescent lamp",
                    ParentId = Guid.Parse("aca1601d-75dd-4108-a77b-631eef70c969"),
                    Description = "Лампа розжарювання задньої протитуманної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9ec8951e-62b1-4c61-b3aa-66d42a5a36cc"),
                    Title = "Tungsten",
                    ParentId = Guid.Parse("d686c65e-d13d-4445-84a0-e5aaec5c80d9"),
                    Description = "Лампа розжарювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("94b58ca8-21c9-4b5c-abbe-0b7449cdcdae"),
                    Title = "Main headlamp incandescent lamp",
                    ParentId = Guid.Parse("a192505b-48d2-494b-a1e0-a6db9ee3740b"),
                    Description = "Лампа розжарювання основної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("798bc635-741d-48a5-9718-9eabe5167f01"),
                    Title = "Main headlamp incandescent lamp",
                    ParentId = Guid.Parse("3f90a5af-50bd-4017-8219-20d549221aa1"),
                    Description = "Лампа розжарювання основної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2ddb8f18-e4a5-427c-971b-e9365ecef9ea"),
                    Title = "High beam headlamp incandescent lamp",
                    ParentId = Guid.Parse("2c1aa106-0387-4c0a-979f-e2e459cf4f95"),
                    Description = "Лампа розжарювання фари дальнього світла",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5fb18871-62c9-4e43-9ccd-baf63cb7a4ac"),
                    Title = "High beam headlamp incandescent lamp",
                    ParentId = Guid.Parse("398ecceb-6908-464a-917c-7927a0dc81bf"),
                    Description = "Лампа розжарювання фари дальнього світла",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c1c05593-172d-4b4e-aaa2-06a22d8aa9c3"),
                    Title = "Lambda Regulation",
                    ParentId = Guid.Parse("76a3f6fe-bd81-4ee3-8fe2-08f07dcbb498"),
                    Description = "Лямбда-регулювання",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7f9d401e-c5be-4ce1-a132-a7399b5057e4"),
                    Title = "oil",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Оливи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b7ffde7f-7da3-4594-9b8f-44f423218200"),
                    Title = "Oil nozzle",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Оливна форсунка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("92778592-6df1-4213-aa9c-6f8e33395ce8"),
                    Title = "Oil pump",
                    ParentId = Guid.Parse("512e79e3-78cf-4c54-b02e-c476f3656c84"),
                    Description = "Оливний насос",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("512e79e3-78cf-4c54-b02e-c476f3656c84"),
                    Title = "Oil pump/attachments",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Оливний насос/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9894141d-8160-4f26-9a5f-1471a1a86eda"),
                    Title = "Oil sump",
                    ParentId = Guid.Parse("d25a9b2a-bce4-4290-b336-ee2395c6b94e"),
                    Description = "Оливний піддон",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d25a9b2a-bce4-4290-b336-ee2395c6b94e"),
                    Title = "Oil sump/attachments",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Оливний піддон/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("eee5ee56-ac46-427d-b66d-60c4c3a50e75"),
                    Title = "Oil cooler",
                    ParentId = Guid.Parse("c7d74817-94eb-498d-8915-8dad8db9c02f"),
                    Description = "Оливний радіатор",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c7d74817-94eb-498d-8915-8dad8db9c02f"),
                    Title = "Oil cooler/hinged components",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Оливний радіатор/навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c12a116f-bbe6-482e-b6d5-3e6d81459754"),
                    Title = "OIL FILTER",
                    ParentId = Guid.Parse("8862fdb4-f8ac-433c-8edb-d3bd1306abbf"),
                    Description = "Оливний фільтр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fdd1fe14-77de-4f42-9d72-ad93766cf3c7"),
                    Title = "OIL FILTER",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Оливний фільтр",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ae5a10e7-4fe9-4d2a-8285-836e6ab2af3d"),
                    Title = "Disc flywheel",
                    ParentId = Guid.Parse("9ae1e561-5e61-4644-a880-f396c611d317"),
                    Description = "Дисковий маховик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Title = "Engine Management System",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Система керування двигуном",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2337a42d-954c-45cf-9a8e-bebafcb2c63f"),
                    Title = "Hinged components",
                    ParentId = Guid.Parse("3429252b-950a-4700-9168-4ea58cabdb56"),
                    Description = "Навісні компоненти",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0e6fb618-1770-4edd-9c92-96428d1aa500"),
                    Title = "Entrance/door jamb",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Вхідний/дверний косяк",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1f984696-75db-4f80-b43b-2db094bff068"),
                    Title = "Entrance/door jamb",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Вхідний/дверний косяк",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("59a7f02a-e0ac-4c7c-8da0-57207e96ebdd"),
                    Title = "Directional/Seal/Valve Adjustment",
                    ParentId = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Description = "Напрямна/ущільнення/регулювання клапана",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6e9235e6-23d3-4ed9-9962-a81cad44e9b5"),
                    Title = "Chain tensioner",
                    ParentId = Guid.Parse("4d4edc30-0ab0-4d7c-a42d-b9cca25cd398"),
                    Description = "Натяжний пристрій ланцюга",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6b0ca428-6c2a-438d-a094-40b4ac2a7b9a"),
                    Title = "Decorative/protective overlays, emblems, splash-proof panels",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Декоративні/захисні накладки, емблеми, бризкозахисні панелі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c25ae583-cc27-4aa0-b7e7-51f2627eb573"),
                    Title = "Decorative/Protective Pads",
                    ParentId = Guid.Parse("3979809f-3a4b-4331-b1c1-ca268e687b5c"),
                    Description = "Декоративні/захисні накладки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0b89058c-c5fc-4a8d-8608-872338995f27"),
                    Title = "Decorative/Protective Pads",
                    ParentId = Guid.Parse("51e3b6e4-9870-4170-bfd0-770a003edc82"),
                    Description = "Декоративні/захисні накладки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1dbb3e97-16fe-4e3c-b397-7faa0b553aa6"),
                    Title = "Decorative/Protective Pads",
                    ParentId = Guid.Parse("6b0ca428-6c2a-438d-a094-40b4ac2a7b9a"),
                    Description = "Декоративні/захисні накладки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("850c9e3d-dc8c-46b6-adec-18421ac03351"),
                    Title = "Sheathing/grating",
                    ParentId = Guid.Parse("3979809f-3a4b-4331-b1c1-ca268e687b5c"),
                    Description = "Обшивка/решітка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("202ffe5c-167a-48e4-94a5-da70f3bc305c"),
                    Title = "Window glass",
                    ParentId = Guid.Parse("3429252b-950a-4700-9168-4ea58cabdb56"),
                    Description = "Віконне скло",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d88b1e61-ac19-4172-8424-4cd473a9b90a"),
                    Title = "Body Floor/Bottom",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Підлога кузова/днище",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("35ad0807-43c0-4e34-b31b-987e87022a58"),
                    Title = "Body Floor/Bottom",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Підлога кузова/днище",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("39dd0fdf-6365-442f-ba66-9f4505467563"),
                    Title = "Main headlight/optical element",
                    ParentId = Guid.Parse("3f90a5af-50bd-4017-8219-20d549221aa1"),
                    Description = "Основна фара/оптичний елемент",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("29459f16-416e-4d8b-b4f0-5144a5dd750d"),
                    Title = "Main headlight/optical element",
                    ParentId = Guid.Parse("a192505b-48d2-494b-a1e0-a6db9ee3740b"),
                    Description = "Основна фара/оптичний елемент",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a192505b-48d2-494b-a1e0-a6db9ee3740b"),
                    Title = "Headlamp/Parts",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Основна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3f90a5af-50bd-4017-8219-20d549221aa1"),
                    Title = "Headlamp/Parts",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Основна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3429252b-950a-4700-9168-4ea58cabdb56"),
                    Title = "Glazing",
                    ParentId = Guid.Parse("136eb99a-6a6b-4806-9c74-a6391a506bb0"),
                    Description = "Засклення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("136eb99a-6a6b-4806-9c74-a6391a506bb0"),
                    Title = "Glazing/mirrors",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Засклення/дзеркала",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5147e22f-3b8f-4f73-a8b6-424fc977a77c"),
                    Title = "Motor damper",
                    ParentId = Guid.Parse("9ad2bb41-7b88-4818-a480-09e370175a6b"),
                    Description = "Демпфер двигуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b96f27b8-d9b9-4a19-a17e-52fdbcd35870"),
                    Title = "Charge air cooler",
                    ParentId = Guid.Parse("5b85feda-23a8-4033-a093-517053bde4ba"),
                    Description = "Охолоджувач наддувального повітря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b08c9e88-fc82-487d-bcca-767a72aa7bed"),
                    Title = "Retractor/guide roller",
                    ParentId = Guid.Parse("624140db-7214-46c5-b449-dd7f8176a408"),
                    Description = "Відвідний/напрямний ролик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ba6aa3ed-3d5a-4f49-adc7-ba9a783d7ed3"),
                    Title = "The windshield",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Лобове скло",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("81ff58fb-47d6-4960-9889-c6b2e64a7201"),
                    Title = "Front skin/front grille",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Передня обшивка/передня решітка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("152645c3-082f-4dd7-941d-602cefc14f61"),
                    Title = "Gleitschiene",
                    ParentId = Guid.Parse("4d4edc30-0ab0-4d7c-a42d-b9cca25cd398"),
                    Description = "Напрямна планка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("72f60686-881c-49c3-b21a-f69d756201b7"),
                    Title = "motor support",
                    ParentId = Guid.Parse("9ad2bb41-7b88-4818-a480-09e370175a6b"),
                    Description = "Опора двигуна",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d7707ebd-f39d-4f5e-a2ca-5e9a4a971529"),
                    Title = "Covers/caps",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Кришки/ковпачки",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f35e0c6c-f037-46e1-b6d9-3603fa634e44"),
                    Title = "Piston",
                    ParentId = Guid.Parse("9ae1e561-5e61-4644-a880-f396c611d317"),
                    Description = "Поршень",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3c825bc4-f864-4170-8cdd-11107b301411"),
                    Title = "Piston",
                    ParentId = Guid.Parse("f35e0c6c-f037-46e1-b6d9-3603fa634e44"),
                    Description = "Поршень",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7abbbe07-b9db-4126-a0cc-f0588fef9574"),
                    Title = "Piston Assembly",
                    ParentId = Guid.Parse("f35e0c6c-f037-46e1-b6d9-3603fa634e44"),
                    Description = "Поршень в зборі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("69c276f8-545e-431e-b5bb-be49dc5b8d96"),
                    Title = "Pressure converter",
                    ParentId = Guid.Parse("00ee2968-d8ba-40e2-ace2-c82ad955025c"),
                    Description = "Перетворювач тиску",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("14b1554a-b890-4781-9b55-83317d61f5fc"),
                    Title = "Valve actuator",
                    ParentId = Guid.Parse("8dcf6465-9e8b-457d-a773-0f24290d5046"),
                    Description = "Привод клапана",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f379c477-af4f-442f-99c9-a4ccdefafa77"),
                    Title = "Drain plug",
                    ParentId = Guid.Parse("d25a9b2a-bce4-4290-b336-ee2395c6b94e"),
                    Description = "Спускна пробка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c2e15dfc-7980-44ba-9348-4aa27125585b"),
                    Title = "Longitudinal/transverse beam",
                    ParentId = Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08"),
                    Description = "Поздовжня/поперечна балка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("19283fad-79e4-4e3a-93e1-47aaf77a67ce"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("c7d74817-94eb-498d-8915-8dad8db9c02f"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("af64c24b-bde7-4760-8a4e-8e5a4faa2c04"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("d25a9b2a-bce4-4290-b336-ee2395c6b94e"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d8c1f97c-5975-4492-a614-4a47482fc44c"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("512e79e3-78cf-4c54-b02e-c476f3656c84"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0f61b6d8-3a58-447d-8cb4-9cdc716949df"),
                    Title = "Intake manifold seal",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення впускного колектора",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("c50811b8-a36c-4d41-8a4b-ae55bded5be1"),
                    Title = "Cylinder head seal",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення голівки циліндра",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3ccba621-b083-4a14-9681-38ac3fbad132"),
                    Title = "Cylinder head seal",
                    ParentId = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Description = "Ущільнення голівки циліндра",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4e0ab95e-ed78-42f1-8c93-931387b7fb91"),
                    Title = "Crankcase seal",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення картера",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("09b72f49-f8d0-42c7-b8d7-1993aa38592c"),
                    Title = "Shaft O-ring/set",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнювальне кільце вала/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("08242285-32c9-4d5e-99a3-24c10bf0fa7d"),
                    Title = "Blower seal",
                    ParentId = Guid.Parse("5b85feda-23a8-4033-a093-517053bde4ba"),
                    Description = "Ущільнення нагнітача",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2a493ecb-98e8-4036-8ff0-2243cdbc3ec2"),
                    Title = "Valve cover seal",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення кришки клапана",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("efd26bbf-6d9f-4d57-97b2-2075f62835f6"),
                    Title = "Sealing of the crankcase of the distribution mechanism",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення картера розподільного механізму",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("fdd9bafc-16ed-456b-8dca-4957b277ac57"),
                    Title = "Oil pan seal",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення оливного піддона",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a259f128-48d9-4433-a6d6-33932fd43314"),
                    Title = "Valve Stem seal",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення штока клапана",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("10e1033f-7831-46e3-840b-b71641b2daeb"),
                    Title = "Turbocharger seal",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення турбонагнітача",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2eb34766-9694-4e2a-bc96-ff6100a2e0af"),
                    Title = "Exhaust manifold seal/O-ring",
                    ParentId = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Description = "Ущільнення випускного колектора/ущільнювальне кільце",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e04102c0-9405-4766-9c04-44c30e65340d"),
                    Title = "Seal inlet/outlet manifold/O-ring",
                    ParentId = Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2"),
                    Description = "Ущільнення впускного/випускного колектора/ущільн. кільце",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf"),
                    Title = "Seal assembly",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Ущільнення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("eb7d93c9-690e-4721-ac7a-02a9e0080391"),
                    Title = "Fog lamp/optical element",
                    ParentId = Guid.Parse("6c771286-4acc-40c1-9a2e-78d2a96abe18"),
                    Description = "Протитуманна фара/оптичний елемент",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("e7f9a1d4-1a73-462b-89e1-c5aa1db0650a"),
                    Title = "Fog lamp/optical element",
                    ParentId = Guid.Parse("a2a04cd6-0a7d-42a7-8020-0a62b9067f18"),
                    Description = "Протитуманна фара/оптичний елемент",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6c771286-4acc-40c1-9a2e-78d2a96abe18"),
                    Title = "Fog lamp/parts",
                    ParentId = Guid.Parse("66ef6f29-b9f4-44c9-9ad8-d7b7f5e2ccfe"),
                    Description = "Протитуманна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a2a04cd6-0a7d-42a7-8020-0a62b9067f18"),
                    Title = "Fog lamp/parts",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Протитуманна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f43e47db-7f84-4a3d-a09a-c43145ab2a1c"),
                    Title = "Fog lamp incandescent lamp",
                    ParentId = Guid.Parse("6c771286-4acc-40c1-9a2e-78d2a96abe18"),
                    Description = "Лампа розжарювання протитуманної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("f64e03ad-88b9-488e-aa06-d436cdbfa755"),
                    Title = "Fog lamp incandescent lamp",
                    ParentId = Guid.Parse("a2a04cd6-0a7d-42a7-8020-0a62b9067f18"),
                    Description = "Лампа розжарювання протитуманної фари",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("305c2a03-4c23-42a7-9a4f-77bdf4191e56"),
                    Title = "Camshaft",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Кулачковий вал",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("3b49beef-307d-48bf-95ea-edcccf145c55"),
                    Title = "Charge air control system",
                    ParentId = Guid.Parse("5b85feda-23a8-4033-a093-517053bde4ba"),
                    Description = "Система регулювання наддувального повітря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("624140db-7214-46c5-b449-dd7f8176a408"),
                    Title = "Toothed belt/tensioning/driving",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Зубчастий ремінь/натягування/ведення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("78e60851-7545-4809-9b29-50dc6eb2ba65"),
                    Title = "HV recirculation control system",
                    ParentId = Guid.Parse("00ee2968-d8ba-40e2-ace2-c82ad955025c"),
                    Description = "Система керування рециркуляцією ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00ee2968-d8ba-40e2-ace2-c82ad955025c"),
                    Title = "HV recirculation system",
                    ParentId = Guid.Parse("76a3f6fe-bd81-4ee3-8fe2-08f07dcbb498"),
                    Description = "Система рециркуляції ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5800d7a8-a41d-4bab-b7a7-f11a06010716"),
                    Title = "Tension roller",
                    ParentId = Guid.Parse("624140db-7214-46c5-b449-dd7f8176a408"),
                    Description = "Натяжний ролик",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("16478017-b8c9-4ead-8469-c742b6477693"),
                    Title = "Radial shaft seal/set",
                    ParentId = Guid.Parse("9ae1e561-5e61-4644-a880-f396c611d317"),
                    Description = "Радіальне ущільнення вала/комплект",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5b85feda-23a8-4033-a093-517053bde4ba"),
                    Title = "Pressurization",
                    ParentId = Guid.Parse("72f3a4b3-c989-4276-9d3d-9ca40a025ee8"),
                    Description = "Наддування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Title = "Lights",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Ліхтарі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("76a3f6fe-bd81-4ee3-8fe2-08f07dcbb498"),
                    Title = "Self Cleaning",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Система очищення ВГ",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("72f3a4b3-c989-4276-9d3d-9ca40a025ee8"),
                    Title = "Air supply system",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Система подачі повітря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Title = "Lubricating Oil System",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Система змащування",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d8d0a1f0-f0b8-4c25-bf70-3867f9e883f5"),
                    Title = "Hoses/Piping",
                    ParentId = Guid.Parse("72f3a4b3-c989-4276-9d3d-9ca40a025ee8"),
                    Description = "Шланги/трубопроводи",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("84868942-43e3-497e-99c2-208146b33433"),
                    Title = "Side-/side-marker lamp/parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Боковий-/габаритний ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("d686c65e-d13d-4445-84a0-e5aaec5c80d9"),
                    Title = "Side-/side-marker lamp/parts",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Боковий-/габаритний ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4001dc55-58f1-4082-be35-835a4dd23337"),
                    Title = "Side-/side-marker lamp/parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Боковий-/габаритний ліхтар/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("6452c062-3e15-4b25-9f90-744a611f82e2"),
                    Title = "Parking/parking light",
                    ParentId = Guid.Parse("d686c65e-d13d-4445-84a0-e5aaec5c80d9"),
                    Description = "Паркувальний/стоянковий ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2bec6cf7-ec07-413a-a5e1-00cb8eba4aa0"),
                    Title = "Parking/parking light",
                    ParentId = Guid.Parse("84868942-43e3-497e-99c2-208146b33433"),
                    Description = "Паркувальний/стоянковий ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b6a9f82a-7c66-4146-8c28-35a488a23deb"),
                    Title = "Parking/parking light",
                    ParentId = Guid.Parse("4001dc55-58f1-4082-be35-835a4dd23337"),
                    Description = "Паркувальний/стоянковий ліхтар",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b341f359-90d2-40b3-9151-e62ee810cdcd"),
                    Title = "Fuel tank/parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Паливний бак/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("02dc97c6-c88f-41d7-920f-5da3efcf704c"),
                    Title = "Fuel tank/parts",
                    ParentId = Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b"),
                    Description = "Паливний бак/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8d8486f8-f798-49e7-8605-4bcf76d00545"),
                    Title = "Fuel tank/parts",
                    ParentId = Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7"),
                    Description = "Паливний бак/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("55013979-22fb-45ac-91ff-39be78e94cd9"),
                    Title = "Fuel filter",
                    ParentId = Guid.Parse("8862fdb4-f8ac-433c-8edb-d3bd1306abbf"),
                    Description = "Фільтр палива",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("7d67964a-9d8b-4fe1-9bc9-d90ec1730cc9"),
                    Title = "Gas pipeline/throttle thrust",
                    ParentId = Guid.Parse("72f3a4b3-c989-4276-9d3d-9ca40a025ee8"),
                    Description = "Газопровід/тяга дроселя",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a1c6e871-6767-493d-a9e8-78c9be997239"),
                    Title = "Charge air hose",
                    ParentId = Guid.Parse("5b85feda-23a8-4033-a093-517053bde4ba"),
                    Description = "Шланг подачі наддувального повітря",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ad6c551d-b1fc-4a93-9c0f-d376214ea823"),
                    Title = "Oil dipstick",
                    ParentId = Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57"),
                    Description = "Оливний щуп",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2c1aa106-0387-4c0a-979f-e2e459cf4f95"),
                    Title = "Headlamp/Parts",
                    ParentId = Guid.Parse("66ef6f29-b9f4-44c9-9ad8-d7b7f5e2ccfe"),
                    Description = "Основна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("398ecceb-6908-464a-917c-7927a0dc81bf"),
                    Title = "Headlamp/Parts",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Основна фара/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("ce3ceec7-55b8-4dff-a11e-9c19696786e9"),
                    Title = "Reversing light/parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Ліхтар заднього ходу/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("87915875-44d9-490d-9c2d-ed2a1703635e"),
                    Title = "Reversing light/parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Ліхтар заднього ходу/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("431cc832-82ac-4cb5-a562-d5faa4c06de7"),
                    Title = "Cabin air filter",
                    ParentId = Guid.Parse("8862fdb4-f8ac-433c-8edb-d3bd1306abbf"),
                    Description = "Повітряний фільтр салону",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("182b30ff-077e-486d-af99-05875c6c22b4"),
                    Title = "License Plate Lantern/Parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Ліхтар номерного знака/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("47d341cf-3516-4be4-ac23-0bf1b59d5144"),
                    Title = "License Plate Lantern/Parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Ліхтар номерного знака/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b280a8f0-53dd-47b6-a369-b9b2e82b0a79"),
                    Title = "Brake Light/Parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Стоп-сигнал/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("5a1a9635-fc82-4af3-9412-d39ead6f9ac4"),
                    Title = "Brake Light/Parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Стоп-сигнал/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("33ae1406-23ba-4f11-8dc2-24dd7e15d55b"),
                    Title = "Turn Signal",
                    ParentId = Guid.Parse("2b039db9-3b2e-4e44-bb99-bd2317d42442"),
                    Description = "Вказівник повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a44c2d67-f8e0-47c8-b5cd-7625f0346263"),
                    Title = "Turn Signal",
                    ParentId = Guid.Parse("acfcf892-ce8b-4e07-9fd7-d5712983c396"),
                    Description = "Вказівник повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("a7ca7faa-9164-4fcd-aa2f-b6d061a402f9"),
                    Title = "Turn Signal",
                    ParentId = Guid.Parse("02c71b5a-a91d-4b81-a29d-4c8eb7b1c0fb"),
                    Description = "Вказівник повороту",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("02c71b5a-a91d-4b81-a29d-4c8eb7b1c0fb"),
                    Title = "Turn Signal/Parts",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Вказівник повороту/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("acfcf892-ce8b-4e07-9fd7-d5712983c396"),
                    Title = "Turn Signal/Parts",
                    ParentId = Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02"),
                    Description = "Вказівник повороту/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2b039db9-3b2e-4e44-bb99-bd2317d42442"),
                    Title = "Turn Signal/Parts",
                    ParentId = Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8"),
                    Description = "Вказівник повороту/деталі",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("176ef6bf-0d71-4fe4-80b8-5edb24692ed0"),
                    Title = "Lantern at the door",
                    ParentId = Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9"),
                    Description = "Ліхтар у дверях",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("82967381-aaa1-4b26-a856-e1b0ae970a49"),
                    Title = "Drive Chain",
                    ParentId = Guid.Parse("512e79e3-78cf-4c54-b02e-c476f3656c84"),
                    Description = "Приводний ланцюг",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("1cb9627d-55ec-4513-9984-b372992e13a5"),
                    Title = "Drive Chain",
                    ParentId = Guid.Parse("4d4edc30-0ab0-4d7c-a42d-b9cca25cd398"),
                    Description = "Ланцюг привода",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("4d4edc30-0ab0-4d7c-a42d-b9cca25cd398"),
                    Title = "Drive/Tensioning/Leading Chain",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Ланцюг привода/натягування/ведення",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("8a0b13f8-1262-47f5-a91f-48d8165651f0"),
                    Title = "Connecting-rod",
                    ParentId = Guid.Parse("aaaf11ef-70f0-4d5e-8b25-9f38c9aae554"),
                    Description = "Шатун",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("28c8ad31-5eab-4a9d-8900-2bbbe0680a35"),
                    Title = "Sprockets",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Зубчасті колеса",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("9043e3dd-2495-4d22-ad45-ec143d89c439"),
                    Title = "Cylindrical gear",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Циліндрична шестірня",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("b7d992bc-cfd3-41bb-a62e-53dd40c3c2d2"),
                    Title = "Pusher/rod/protective tube",
                    ParentId = Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345"),
                    Description = "Штовхач/штанга/захисна трубка",
                    Timestamp = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("2e925d9d-8410-4e66-9fa8-e7d2302c75c4"),
                    Title = "Motor Electrical System",
                    ParentId = Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02"),
                    Description = "Електрична система двигуна",
                    Timestamp = DateTime.UtcNow
                }
            };
        }

        // Мапінг старих ID на нові GUID для довідки
        public static Dictionary<int, Guid> GetIdMapping()
        {
            return new Dictionary<int, Guid>
            {
                { 100240, Guid.Parse("9ff89900-8cc4-4787-a592-e636a095155b") },
                { 100238, Guid.Parse("99f47e60-8abb-4c83-b5f6-b9c20b34fe62") },
                { 101705, Guid.Parse("42bc8bdd-4003-47e5-85eb-e65fa96b33d8") },
                { 100001, Guid.Parse("2d453935-b539-436b-bce7-7d44d4fca8f7") },
                { 101648, Guid.Parse("dbb4ac83-ceb2-4b8b-a02f-18f429dbbf02") },
                { 100614, Guid.Parse("24241228-01d0-4f54-8b58-28d941b6aebc") },
                { 100335, Guid.Parse("5ff5f792-76bd-4db6-b696-a891c7b299ca") },
                { 100121, Guid.Parse("74a41599-1ab1-4e4f-b69d-bf1a6c589c61") },
                { 100011, Guid.Parse("624d4c0e-10b5-4852-a40a-cf5a7bd4191e") },
                { 100695, Guid.Parse("f86e7d67-166d-4a33-af4f-72f78a51a27a") },
                { 100339, Guid.Parse("00c0efc7-4c40-42b1-9ebc-b02bea62e161") },
                { 102205, Guid.Parse("751056a9-4261-411e-aa05-6d5bb8336823") },
                { 100007, Guid.Parse("99b1e4b9-9272-4e21-851f-09914f3fb174") },
                { 100694, Guid.Parse("69d40b9b-890c-4d8d-bc41-0c1c1dcf80fc") },
                { 102697, Guid.Parse("88ed0184-e8af-4d9a-8d86-a7457aebf3a4") },
                { 100341, Guid.Parse("70ee9a8a-bd6e-48d3-aff6-1c784a594c00") },
                { 100862, Guid.Parse("e59b0edb-84a0-452e-bb43-f05898dd8249") },
                { 100733, Guid.Parse("c716307a-76b6-4feb-a31d-c2c3691a885d") },
                { 100761, Guid.Parse("4f8d3c98-4368-4265-84e9-50b0d190b554") },
                { 100212, Guid.Parse("76441a56-2b00-4c58-bc58-b00c65d6943d") },
                { 100013, Guid.Parse("b67d37d0-7ce2-4809-a429-655e6010487e") },
                { 100627, Guid.Parse("aacbf15d-0ee9-4779-9cf8-161fdd67fbd8") },
                { 100006, Guid.Parse("6389a0dc-debe-49cd-889e-91739ae338f6") },
                { 100042, Guid.Parse("b0b515ad-4e48-4920-ba1a-24dc1af39628") },
                { 100010, Guid.Parse("b820bedd-adb2-4834-b7ff-48d8d243f712") },
                { 100610, Guid.Parse("0ad1d1bc-a1a0-459e-97fd-feffea06a259") },
                { 100018, Guid.Parse("dd24bbdc-fb82-435c-9dd8-1f9d8327c093") },
                { 100514, Guid.Parse("b55e1df7-ebef-4d7c-a580-89af2475d8cb") },
                { 100002, Guid.Parse("a0ad9b2e-78f3-4196-9384-9e15134fba02") },
                { 100533, Guid.Parse("2b770362-38c5-4b2a-8671-e6a1c90d2388") },
                { 100725, Guid.Parse("b54814af-1485-46bb-963a-3677663a8c37") },
                { 100241, Guid.Parse("60e2cee8-2d53-4dd1-9838-cdfd3ddcbd87") },
                { 102810, Guid.Parse("04e35c25-9af3-47f8-8b8d-c0686cf5f43d") },
                { 102793, Guid.Parse("5d8779b4-df2e-4f45-b088-5fcf65f07841") },
                { 100247, Guid.Parse("e0eecc85-b117-456c-813a-4f25add9a887") },
                { 100008, Guid.Parse("a149b62c-548e-44e1-9e96-f312d9b08577") },
                { 101696, Guid.Parse("7aa43e11-b0d3-4a8f-b92b-8b3665d9e3db") },
                { 101690, Guid.Parse("5ea155cd-302c-439d-a5d3-20432903255b") },
                { 100181, Guid.Parse("67cc9768-0cf5-4b4b-b6f6-3e868c546162") },
                { 100743, Guid.Parse("37903d53-1fd2-4859-8d6f-440983bfaa08") },
                { 101780, Guid.Parse("612c1495-9a7e-491a-bc42-493f1a81339f") },
                { 102406, Guid.Parse("08db2e69-2c2c-400f-b2f4-7e281956b190") },
                { 101420, Guid.Parse("02c71b5a-a91d-4b81-a29d-4c8eb7b1c0fb") },
                { 100528, Guid.Parse("dbd289b2-f333-45e5-94fa-e9964f0dbaec") },
                { 100519, Guid.Parse("aaaf11ef-70f0-4d5e-8b25-9f38c9aae554") },
                { 100378, Guid.Parse("182338f3-eb50-4986-ba94-0a42a01bc5b4") },
                { 100373, Guid.Parse("d506c3f4-ad6c-4385-8b19-ddcddecbc0a2") },
                { 100333, Guid.Parse("c97336d2-135c-4cca-91a6-3b1c7bc53501") },
                { 100317, Guid.Parse("5db7b282-faee-4b17-8082-55e95bda9dc5") },
                { 100187, Guid.Parse("a85a437f-715c-473d-870c-e9b290feb4ff") },
                { 101649, Guid.Parse("13ccf5d1-0362-490c-8c6e-0852da2ad9e6") },
                { 100337, Guid.Parse("37b90a0a-30aa-4e4a-abad-2efa714286c4") },
                { 100876, Guid.Parse("004ec141-c431-4881-b682-c08cd3fdf3f7") },
                { 100082, Guid.Parse("b34430e1-3873-4ce5-b303-da8aee3e4533") },
                { 100525, Guid.Parse("7e397d12-c68d-4ac7-b235-343447d781f7") },
                { 100522, Guid.Parse("c1ff46b4-c012-459d-9d4c-3000af501383") },
                { 100517, Guid.Parse("d859a756-cb15-46ae-8aa6-587bfd8ff6e7") },
                { 102346, Guid.Parse("20047c4d-f70c-4ce0-9d2d-12fb364d0474") },
                { 100745, Guid.Parse("3429252b-950a-4700-9168-4ea58cabdb56") },
                { 100687, Guid.Parse("98e2fac7-30bc-46ad-9fd0-23e0124930fa") },
                { 100685, Guid.Parse("d85cd4d4-06a5-41c8-87ed-d75d380b096b") },
                { 100292, Guid.Parse("f39dd37c-1b3f-4525-9610-7f1220474117") },
                { 100043, Guid.Parse("08f91863-f9d7-4d86-875a-2f71214879f6") },
                { 100688, Guid.Parse("01c913d3-7d2a-48a6-ae0d-578805fc917f") },
                { 100102, Guid.Parse("9b787865-af44-45d3-92c4-1d5d9d5af88b") },
                { 100091, Guid.Parse("42601cc7-3f34-4b4b-9248-815da984c8e7") },
                { 100088, Guid.Parse("a0c82443-a95e-4e82-bd5c-c56b14c21a32") },
                { 100135, Guid.Parse("556e018b-400a-463e-8e34-193742893c3a") },
                { 100103, Guid.Parse("e05db368-4d62-43cd-a075-62f9b80cf91b") },
                { 100260, Guid.Parse("216df717-d107-4e5c-9ba4-58952e91533c") },
                { 100005, Guid.Parse("8862fdb4-f8ac-433c-8edb-d3bd1306abbf") },
                { 100384, Guid.Parse("b7292803-8f94-4ecc-98dd-58b4ea6fd59c") },
                { 100383, Guid.Parse("72f3a4b3-c989-4276-9d3d-9ca40a025ee8") },
                { 100386, Guid.Parse("6abecb7a-4bbe-4e28-9b43-34a8df3681e9") },
                { 101504, Guid.Parse("2bab86fd-bc19-4cd1-85fd-24d243426a31") },
                { 100716, Guid.Parse("ab88ee67-5bef-45ef-9ce6-ccd1a4b222ae") },
                { 100527, Guid.Parse("6ee7fdbb-262a-4eaa-bffc-eadc58f6a4f4") },
                { 100366, Guid.Parse("2e01571b-ecf3-4b50-86cc-ba539a65ec5a") },
                { 100243, Guid.Parse("746b3dec-71cd-41a7-902e-4404a29fc5f2") },
                { 100509, Guid.Parse("b6023dfc-bd62-45ee-b2e4-a7c45d01362a") },
                { 100307, Guid.Parse("c9137508-997c-44f1-83b1-8017e8764121") },
                { 100334, Guid.Parse("908769ce-d05c-4d4f-bed2-b9c321854b9d") },
                { 100613, Guid.Parse("dbd271e4-e5bf-4512-93be-c9fb6bc36b65") },
                { 102808, Guid.Parse("92ecf094-4c02-48c6-acb7-4d13c7ae8921") },
                { 102248, Guid.Parse("faba878d-b6d3-4f00-b9b1-bd46df55fe33") },
                { 101787, Guid.Parse("4aaf5d56-2e3c-4072-93f5-68f2a90af6d8") },
                { 101785, Guid.Parse("4001dc55-58f1-4082-be35-835a4dd23337") },
                { 100469, Guid.Parse("6e0faf33-074f-483c-83ad-59950c256458") },
                { 100290, Guid.Parse("ce0c13c1-056b-414d-b7cf-aed9e7bfa2b2") },
                { 101768, Guid.Parse("ca453283-67c0-405a-8d92-a93e1d2fcf8a") },
                { 101766, Guid.Parse("d686c65e-d13d-4445-84a0-e5aaec5c80d9") },
                { 101443, Guid.Parse("1a08dcc5-4760-44cb-b861-f11f33b31f3f") },
                { 101441, Guid.Parse("84868942-43e3-497e-99c2-208146b33433") },
                { 101718, Guid.Parse("39b348d7-c668-48b1-a783-b73afe728a80") },
                { 101411, Guid.Parse("b01c7796-0bbf-4a1a-b8bd-2f39a2de8c4e") },
                { 101407, Guid.Parse("2d9f3164-7dee-4bd7-8972-a32193f1f6d9") },
                { 101752, Guid.Parse("feca2ad8-302a-4402-aa21-63505709f97b") },
                { 101745, Guid.Parse("d90bbbdd-2e2b-43f2-9119-41e5d34d670e") },
                { 101703, Guid.Parse("ad166207-f418-4dc3-bb00-11ff6bbcb435") },
                { 101700, Guid.Parse("51e3b6e4-9870-4170-bfd0-770a003edc82") },
                { 100826, Guid.Parse("72c138ce-1c8d-4c12-b231-ff5060d879a3") },
                { 100350, Guid.Parse("8dda310d-56b4-4657-a4c5-756fde48a51b") },
                { 100041, Guid.Parse("2a398538-71de-40a3-8593-0d51d2e6a9da") },
                { 100249, Guid.Parse("e27ba410-9b8c-4c5a-a1c8-1d29c6fe92e6") },
                { 100684, Guid.Parse("bd34ca56-2bb2-4c62-8c14-7a8d787943e5") },
                { 100223, Guid.Parse("94963de6-db1c-441f-9c4f-db29fd7f2fcf") },
                { 100683, Guid.Parse("b8094280-b417-4d69-935f-0aaf19eb1275") },
                { 100682, Guid.Parse("fb07cfa9-af7f-461f-baa8-3918104702a3") },
                { 100534, Guid.Parse("3c5c9e2f-1e34-4d22-b682-4ab19d3370a8") },
                { 100400, Guid.Parse("12fdbf9d-f2ca-41f8-8c87-6928bfdadc0d") },
                { 100026, Guid.Parse("0da13f48-91af-4e57-94be-32280b3be9aa") },
                { 100061, Guid.Parse("518fed00-f314-4475-9f24-4ea51e5260a6") },
                { 100079, Guid.Parse("2c0b86fc-67ab-4573-a51e-f686ff4b2334") },
                { 100314, Guid.Parse("78d8a9a8-de24-4e78-9021-d57cb6975764") },
                { 100004, Guid.Parse("c5d3ab30-8185-4284-899c-ddf7c5d44bce") },
                { 100046, Guid.Parse("25b5f7e2-8bd3-420b-986e-aa560e9f0e78") },
                { 100401, Guid.Parse("f7b8ba20-a20f-4622-807b-f68548798ba5") },
                { 100003, Guid.Parse("d3a57021-e88c-4b82-a19d-247fe05f8345") },
                { 100380, Guid.Parse("729b5f86-a05e-4b08-a193-eb068e5a6f0f") },
                { 100201, Guid.Parse("12f4e593-2367-4715-acaf-82a61cd0ab90") },
                { 100012, Guid.Parse("c2afd28b-4377-43f2-943b-19d0e26b0e33") },
                { 102930, Guid.Parse("c5efe932-fd10-47c5-b5f0-2a7ae613a706") },
                { 100239, Guid.Parse("9f982650-9664-4e10-aea0-e8bc6cc08196") },
                { 100478, Guid.Parse("d2efb523-d640-4c5a-aa65-8848e80ae0ef") },
                { 100245, Guid.Parse("0d9c4501-c100-47e4-a334-c76ad5994a57") },
                { 102809, Guid.Parse("1438d047-3346-44fc-b06e-5a0f9a9cc067") },
                { 103414, Guid.Parse("36cd8d37-a526-469c-bbbf-5cd4b9f10d39") },
                { 100794, Guid.Parse("187e4a5a-856f-47d4-a5cc-89bacea479b4") },
                { 102747, Guid.Parse("c024b14e-3c46-4c30-b3e6-dcb1d8600d77") },
                { 100631, Guid.Parse("60d2b89f-01ca-4af6-b1e5-60888a727b57") },
                { 100553, Guid.Parse("c05b7693-c9c1-483f-b50b-3c148e83d8a1") },
                { 100322, Guid.Parse("c9e80e8a-2bb2-4f8c-9074-1c1881428e6f") },
                { 102806, Guid.Parse("b1fbe06c-f121-4616-a7b2-80e19e9f00f6") },
                { 100189, Guid.Parse("48e350b7-dbf7-49a1-a59c-6df718b079f8") },
                { 100746, Guid.Parse("9b0782a0-895b-42f1-8ac2-f46ab8e83dac") },
                { 101694, Guid.Parse("f728dc70-4201-423c-9fca-ca1e56af87f8") },
                { 101697, Guid.Parse("54b6c928-e45b-4982-ac6e-f6cacc439259") },
                { 100349, Guid.Parse("80d1c3d2-125b-4648-b207-8aae34df938e") },
                { 104156, Guid.Parse("ae0e812b-000b-4f0f-9fb6-1e379bd41d1f") },
                { 100134, Guid.Parse("c6ac633e-ed3a-40e5-bfa2-be47ba005c62") },
                { 100019, Guid.Parse("673fa4b1-607f-4e76-affb-16df47eb6461") },
                { 100596, Guid.Parse("feba373d-fb2a-41eb-bd81-107e5807cced") },
                { 100210, Guid.Parse("6184d8e6-986d-405f-9575-4cfd010fc4d1") },
                { 101673, Guid.Parse("3979809f-3a4b-4331-b1c1-ca268e687b5c") },
                { 100222, Guid.Parse("ad0d142b-ab2a-4b20-8837-990138f713cc") },
                { 100759, Guid.Parse("bdc66ff4-6bbd-4fe3-a4cb-c2e914b0cf47") },
                { 100626, Guid.Parse("751baa67-0e3d-45fb-bb38-3830a86fff7d") },
                { 100707, Guid.Parse("2c084035-bc85-489d-9efc-94316653c39c") },
                { 100703, Guid.Parse("096999e2-fb81-4898-a4d9-c92d6070c17e") },
                { 100053, Guid.Parse("caf279b5-5ae5-4d1c-a6e2-c281eb2327a9") },
                { 100050, Guid.Parse("b59a6996-070c-4cd7-b0b5-f9e335594c77") },
                { 100415, Guid.Parse("f144a7ad-845f-438f-88e7-275eb7806ac5") },
                { 100870, Guid.Parse("96e47e82-d6ba-4223-b615-4be02b5be94c") },
                { 101340, Guid.Parse("66ef6f29-b9f4-44c9-9ad8-d7b7f5e2ccfe") },
                { 100787, Guid.Parse("ea99f683-f0a6-44c2-b45c-c016286c4662") },
                { 100598, Guid.Parse("133ae2c0-7a45-489d-8f4f-13ebeee4c6a8") },
                { 101699, Guid.Parse("d8a522ec-8e9d-475a-a13a-68a97570376f") },
                { 100867, Guid.Parse("88e6bdfb-1018-48ed-928d-eb177989bceb") },
                { 100284, Guid.Parse("8f5d5679-40fd-4f73-84ef-fc7c88abc9a8") },
                { 101416, Guid.Parse("0c5a1faa-c003-4a03-870a-ac0ca2cc76f0") },
                { 101415, Guid.Parse("b280a8f0-53dd-47b6-a369-b9b2e82b0a79") },
                { 101723, Guid.Parse("1f6c2465-26e4-44d9-a136-12808058613f") },
                { 101722, Guid.Parse("5a1a9635-fc82-4af3-9412-d39ead6f9ac4") },
                { 103200, Guid.Parse("b6a898e9-5e37-44a3-a59d-547030a4a286") },
                { 103198, Guid.Parse("219a2aab-bd01-46be-aebb-68032405a7f2") },
                { 101784, Guid.Parse("4500e132-44d0-46de-a8da-1debd9c120b3") },
                { 101716, Guid.Parse("5ab2bcd9-d459-4245-bb70-cf1bb7981686") },
                { 101715, Guid.Parse("fe24f92e-cac4-4546-a711-05557ddf13c8") },
                { 100454, Guid.Parse("8c04ca8a-fe07-434d-95c8-ba53f6f22b70") },
                { 100275, Guid.Parse("d32e1b0d-8a6b-411f-a46f-7a7b996ea2f6") },
                { 101409, Guid.Parse("0130ecdb-b77f-4ecd-86a9-f5f708f660e9") },
                { 101408, Guid.Parse("948c1715-8bb3-4932-b87b-0a2fa511725d") },
                { 101781, Guid.Parse("b58ab03b-531c-4562-bba9-a42cba3b6e04") },
                { 101735, Guid.Parse("4a670e7e-b891-44ad-aad9-1e0513f04ea7") },
                { 101428, Guid.Parse("aca1601d-75dd-4108-a77b-631eef70c969") },
                { 100285, Guid.Parse("342686dd-5784-450f-9951-31ff408f3885") },
                { 100329, Guid.Parse("6f8bbe23-b3d3-4803-8a40-f9539bd1ee38") },
                { 100320, Guid.Parse("5d2b0a77-c268-42e6-be7e-767a8720d28c") },
                { 100566, Guid.Parse("43312104-9684-465c-8806-95b0bdc711ae") },
                { 100744, Guid.Parse("136eb99a-6a6b-4806-9c74-a6391a506bb0") },
                { 101698, Guid.Parse("71193141-499b-4295-8e83-114233d54ace") },
                { 104159, Guid.Parse("797b3f18-4aac-4622-b100-88d9dbba4d76") },
                { 103159, Guid.Parse("fd187043-9d6c-480a-87b4-ecced0613b34") },
                { 103150, Guid.Parse("3cc91701-cad2-41ea-b097-ebd291e9526b") },
                { 100597, Guid.Parse("a4d4459f-3832-4871-ad60-c25853235790") },
                { 100356, Guid.Parse("b3808537-81c1-48f6-b7f0-f127aa54c594") },
                { 103066, Guid.Parse("67468c21-f260-48a0-9b8b-ad1cc9f6e678") },
                { 101783, Guid.Parse("7c1b9f7c-0e5a-4c4d-98ee-da9c333b860e") },
                { 101654, Guid.Parse("5699f1bd-d1d1-4554-a7ec-0f18ca44deab") },
                { 100184, Guid.Parse("f148c314-a8c8-4204-9f19-c4348a9f5e9c") },
                { 102906, Guid.Parse("0aede8ee-2f3c-4ae6-9e52-6b7dd6470bf1") },
                { 102903, Guid.Parse("b05d3151-70cf-417e-a9ba-d393d8a28ddc") },
                { 100071, Guid.Parse("10763d32-ec2e-40f6-b4e2-098995c4a6ca") },
                { 100014, Guid.Parse("8ebe5047-e7a7-4003-83af-9abb08fb7b41") },
                { 100737, Guid.Parse("6d4b92e1-538b-4e52-a63c-c44a753f199d") },
                { 101691, Guid.Parse("2e3fb39c-588b-44bf-ba6d-efaa613a8134") },
                { 100047, Guid.Parse("cb86681f-7c64-44e1-bd1d-f1276078f036") },
                { 100422, Guid.Parse("8dcf6465-9e8b-457d-a773-0f24290d5046") },
                { 103168, Guid.Parse("71b79f78-0e98-47fb-a8d7-39f49be6bb80") },
                { 100811, Guid.Parse("c5c3cdbe-c011-400c-8d93-c81fb8d87672") },
                { 100791, Guid.Parse("a0e069e6-5472-48ce-b7d1-1c2c08ee986e") },
                { 100551, Guid.Parse("7f93ea54-2958-429e-b7ef-c7d8d4b8c5ec") },
                { 100542, Guid.Parse("00ee2968-d8ba-40e2-ace2-c82ad955025c") },
                { 102219, Guid.Parse("5217ee08-d927-4386-9b4f-fa8d2bb0124c") },
                { 102799, Guid.Parse("72ce0140-183c-4b4a-a0e4-4f78080d9016") },
                { 100358, Guid.Parse("dcea0e93-3dae-4588-a8bd-b50f6f6bc0cf") },
                { 100423, Guid.Parse("da48bd19-be0b-4f0b-bf27-a4b97badf4e2") },
                { 100816, Guid.Parse("a457bae7-77c2-4bad-ae8b-305e94990f1b") },
                { 100549, Guid.Parse("f4ec6257-37cd-444a-b93d-a06b156b5182") },
                { 102811, Guid.Parse("b5b27bfa-0055-4c59-ba18-3dbcd7fd7c33") },
                { 100450, Guid.Parse("707237fb-b179-4bc3-ad6f-562fdada791c") },
                { 100080, Guid.Parse("60ea012e-5c35-46f9-adc7-2f02ddb247aa") },
                { 100016, Guid.Parse("af1a381f-7c3e-4665-b0fe-f0599f402c24") },
                { 103036, Guid.Parse("944e35d7-bddb-452a-b621-d3bd50689e70") },
                { 103037, Guid.Parse("a20c3baa-4385-4c0c-84bd-1e580bf35852") },
                { 103035, Guid.Parse("d6ce2a48-a7dc-4b5c-a156-03316a0e1598") },
                { 102683, Guid.Parse("625a28b8-32ce-47f7-8f66-a13fba56b466") },
                { 100515, Guid.Parse("9ae1e561-5e61-4644-a880-f396c611d317") },
                { 103099, Guid.Parse("ef816943-e43a-4ac2-9d58-95b0f67d7554") },
                { 101710, Guid.Parse("fe1265f8-2e86-43c7-ab0b-b1e3e193a97c") },
                { 101652, Guid.Parse("379e1f77-26cc-4027-8baf-d1cfc367daa8") },
                { 100165, Guid.Parse("ab1b95a6-ac1e-430d-b6b8-ae156f118d5b") },
                { 102965, Guid.Parse("23a6825f-aa0a-4879-890a-583d7690bf6a") },
                { 100028, Guid.Parse("4ab5fac4-bb96-4de8-a4be-37cccc3bcd79") },
                { 100213, Guid.Parse("6d293103-d2fc-4d05-ac91-d4a780eb762b") },
                { 100715, Guid.Parse("5c4c6d09-6738-47b2-ab76-a417870974ed") },
                { 100202, Guid.Parse("f150521b-4ac5-405e-8fbe-0fa7c788a520") },
                { 100696, Guid.Parse("9dd40059-c3b6-4113-aa34-7aa41a00cc70") },
                { 100531, Guid.Parse("7e700b88-4b47-4ec5-a54b-8d9171a8d4b7") },
                { 100520, Guid.Parse("f35e0c6c-f037-46e1-b6d9-3603fa634e44") },
                { 100224, Guid.Parse("9e55b858-a1ae-4060-9764-148eef6508bc") },
                { 100432, Guid.Parse("5086c386-2467-4664-ac5b-b91e465957d8") },
                { 100051, Guid.Parse("e55f8e47-5411-4095-89de-63c3007fbdca") },
                { 102244, Guid.Parse("7e73e60d-d0da-4d51-8100-9de296e523dc") },
                { 100806, Guid.Parse("a3bb2099-5708-486e-b6fe-1b293039d8c4") },
                { 100027, Guid.Parse("d8d6661f-059b-45c2-86fa-da79ba78423e") },
                { 103102, Guid.Parse("094c63ad-291d-4ab7-bfed-424f9f4d5cfc") },
                { 100730, Guid.Parse("fceef809-e5b6-4ca7-a0b2-2953ff9f7976") },
                { 100630, Guid.Parse("f9ec579a-6598-49f5-8d1a-2b5eb4c1f6ae") },
                { 100634, Guid.Parse("f854e9f6-d212-44d9-82be-b03f2261da90") },
                { 102901, Guid.Parse("6d80e72e-bd28-43bc-81c3-843fac26e5f6") },
                { 103278, Guid.Parse("aa6de629-4cf3-443d-926d-5284fbae00c9") },
                { 100403, Guid.Parse("4d4edc30-0ab0-4d7c-a42d-b9cca25cd398") },
                { 100354, Guid.Parse("538af3df-044a-4e56-b27c-901347834326") },
                { 100391, Guid.Parse("b5b4b149-fd40-432f-ae33-bbb7b584018d") },
                { 100390, Guid.Parse("5b85feda-23a8-4033-a093-517053bde4ba") },
                { 100355, Guid.Parse("4ed6968e-f62b-4fbf-ae0c-237ee64e5362") },
                { 101812, Guid.Parse("cc22c0b7-4a14-4fa3-bce1-2ad7afcb0564") },
                { 100472, Guid.Parse("645d4abd-cb0b-451e-a63c-973ad1d8189b") },
                { 100066, Guid.Parse("9fbcefd2-29a8-436f-a30f-c93d7c31dbfa") },
                { 100535, Guid.Parse("9ad2bb41-7b88-4818-a480-09e370175a6b") },
                { 101773, Guid.Parse("31f9a9e6-16fd-4026-8f40-28bcab32d645") },
                { 101454, Guid.Parse("0e5e7e50-a504-4ff0-8c02-111c0955b8f0") },
                { 100327, Guid.Parse("699a2268-6efb-4f6c-b9de-306b26b50e87") },
                { 101779, Guid.Parse("a270bb47-d9c6-4090-9fb6-1c59be69aae7") },
                { 101650, Guid.Parse("e54e1e1d-d3fc-48a6-b1fe-7196e8bad928") },
                { 100185, Guid.Parse("af3f84f2-2153-491e-8db2-3cd5411b3edb") },
                { 100375, Guid.Parse("a745dcee-a16b-417e-935c-c1920188a13a") },
                { 100379, Guid.Parse("db872175-cf00-4079-837e-f99c1b04b336") },
                { 101803, Guid.Parse("5dffee01-f846-4901-bf86-977fe3be8b62") },
                { 101445, Guid.Parse("86f6f8b4-c811-461b-9848-9692792734b9") },
                { 100499, Guid.Parse("aacd346d-7205-4272-8dfe-1c9b93bb25b7") },
                { 100147, Guid.Parse("ac572b78-6498-4c86-9ccd-2d17bc2c4f90") },
                { 101435, Guid.Parse("d9139c44-0247-4265-b280-d500080665c8") },
                { 101432, Guid.Parse("ce3ceec7-55b8-4dff-a11e-9c19696786e9") },
                { 101427, Guid.Parse("4a03bc4a-e53c-4416-b94b-6677dac6ef57") },
                { 101424, Guid.Parse("47d341cf-3516-4be4-ac23-0bf1b59d5144") },
                { 100501, Guid.Parse("47fd9365-bf20-4d8e-8b07-f6fe29e474f1") },
                { 100287, Guid.Parse("e0ad65d7-419e-4cc9-9af2-75e9479f28b0") },
                { 101419, Guid.Parse("1c3f4b3d-3190-4145-b141-daac113666b8") },
                { 101423, Guid.Parse("0804ba66-5ac1-49c5-808e-f3ef156d5151") },
                { 101672, Guid.Parse("334b171b-e2c8-409d-bf23-6161041c3f2d") },
                { 101669, Guid.Parse("acfcf892-ce8b-4e07-9fd7-d5712983c396") },
                { 100500, Guid.Parse("2ede01b4-9956-4a29-b465-39754287294c") },
                { 100503, Guid.Parse("1802b2db-df73-42de-8379-473aae5d596e") },
                { 101721, Guid.Parse("c04fce90-de9f-464f-8ca4-30265105ebcc") },
                { 101738, Guid.Parse("24ff0524-9c1f-450c-bd38-402c5f096d2a") },
                { 101789, Guid.Parse("c8300086-1f8d-45af-8e8a-f4f3e48e1ae7") },
                { 101742, Guid.Parse("80d0a20d-da25-4d77-ad20-f9aa772d5eb2") },
                { 101739, Guid.Parse("87915875-44d9-490d-9c2d-ed2a1703635e") },
                { 101734, Guid.Parse("53cb0ce9-ef45-4623-87db-3d9e094a0dde") },
                { 101731, Guid.Parse("182b30ff-077e-486d-af99-05875c6c22b4") },
                { 101726, Guid.Parse("4c904767-df8f-4109-ae7f-fe8b9a9c790b") },
                { 101730, Guid.Parse("05bbfee8-878e-4972-9e26-58e4b079ecd4") },
                { 101727, Guid.Parse("2b039db9-3b2e-4e44-bb99-bd2317d42442") },
                { 101414, Guid.Parse("8374dbc4-3b24-4dd3-b24b-9f18275e322d") },
                { 100498, Guid.Parse("b78c14ef-a218-4093-baf3-1bfb194024db") },
                { 100145, Guid.Parse("f844f426-c6db-4d88-84aa-ac0111683890") },
                { 101431, Guid.Parse("596e2ac3-c384-4746-8838-d7a1cef2f27f") },
                { 100496, Guid.Parse("24df10c4-365c-4076-a96f-3fba1862fea1") },
                { 101770, Guid.Parse("9ec8951e-62b1-4c61-b3aa-66d42a5a36cc") },
                { 101495, Guid.Parse("ffb1ed1a-d2fa-4166-bcd9-a8a8d442326d") },
                { 100427, Guid.Parse("cf10f55b-5571-4b33-9d43-207bed508975") },
                { 100143, Guid.Parse("d6cecb15-9faf-488e-9a26-1e327d2d46f7") },
                { 101467, Guid.Parse("94b58ca8-21c9-4b5c-abbe-0b7449cdcdae") },
                { 101465, Guid.Parse("a192505b-48d2-494b-a1e0-a6db9ee3740b") },
                { 101658, Guid.Parse("798bc635-741d-48a5-9718-9eabe5167f01") },
                { 101656, Guid.Parse("3f90a5af-50bd-4017-8219-20d549221aa1") },
                { 101347, Guid.Parse("2ddb8f18-e4a5-427c-971b-e9365ecef9ea") },
                { 101345, Guid.Parse("2c1aa106-0387-4c0a-979f-e2e459cf4f95") },
                { 101667, Guid.Parse("5fb18871-62c9-4e43-9ccd-baf63cb7a4ac") },
                { 101665, Guid.Parse("398ecceb-6908-464a-917c-7927a0dc81bf") },
                { 100442, Guid.Parse("e2a510c0-164b-4051-b13f-ae61763851f2") },
                { 100276, Guid.Parse("7e8f04bd-ede0-4297-8746-41c59786115b") },
                { 100048, Guid.Parse("03bb03b0-6355-41cd-aa01-ccbce8da2a3d") },
                { 100793, Guid.Parse("ab58ef48-fb11-4caf-ba47-36a5998728f0") },
                { 100790, Guid.Parse("ac6fecfb-ef1f-4009-bcfb-56f45d26618d") },
                { 100729, Guid.Parse("c1c05593-172d-4b4e-aaa2-06a22d8aa9c3") },
                { 100541, Guid.Parse("76a3f6fe-bd81-4ee3-8fe2-08f07dcbb498") },
                { 102203, Guid.Parse("af417b59-5ab4-4103-92e1-d377e397f502") },
                { 101996, Guid.Parse("bd2bf2da-ee00-413d-8e06-1966b3f717d8") },
                { 101994, Guid.Parse("7f9d401e-c5be-4ce1-a132-a7399b5057e4") },
                { 103072, Guid.Parse("b7ffde7f-7da3-4594-9b8f-44f423218200") },
                { 100492, Guid.Parse("92778592-6df1-4213-aa9c-6f8e33395ce8") },
                { 100476, Guid.Parse("512e79e3-78cf-4c54-b02e-c476f3656c84") },
                { 101854, Guid.Parse("2d7cfb1e-c0e7-4b16-84d3-a27889ec1a1f") },
                { 101853, Guid.Parse("bf5913e9-efa8-4617-98b7-63aa2cdc0353") },
                { 100489, Guid.Parse("9894141d-8160-4f26-9a5f-1471a1a86eda") },
                { 100475, Guid.Parse("d25a9b2a-bce4-4290-b336-ee2395c6b94e") },
                { 100108, Guid.Parse("b5ef1cc0-5cc2-4d56-9294-fffbb10a0b3a") },
                { 100483, Guid.Parse("eee5ee56-ac46-427d-b66d-60c4c3a50e75") },
                { 100474, Guid.Parse("c7d74817-94eb-498d-8915-8dad8db9c02f") },
                { 100259, Guid.Parse("c12a116f-bbe6-482e-b6d5-3e6d81459754") },
                { 100470, Guid.Parse("fdd1fe14-77de-4f42-9d72-ad93766cf3c7") },
                { 100057, Guid.Parse("81c38ec8-177a-4291-b198-0075b1cb06b2") },
                { 100518, Guid.Parse("ae5a10e7-4fe9-4d2a-8285-836e6ab2af3d") },
                { 102239, Guid.Parse("03da70d2-1199-4dc1-b889-5546be965097") },
                { 103428, Guid.Parse("35fc035b-a493-4a71-b4c0-5f7357d5359b") },
                { 100316, Guid.Parse("b14e64c2-558e-439b-b14d-2f71c16bd91f") },
                { 100569, Guid.Parse("2337a42d-954c-45cf-9a8e-bebafcb2c63f") },
                { 100372, Guid.Parse("2fad5ffa-4d83-436e-80e0-e7c56a08c9c4") },
                { 100381, Guid.Parse("e674620b-e9a2-4bd8-930b-96d610776880") },
                { 100872, Guid.Parse("5f64cb6b-a0fc-4e18-844a-39072526ca5e") },
                { 100052, Guid.Parse("9e79b62c-607b-4eaf-8ea6-3f0f09e1a337") },
                { 101692, Guid.Parse("0e6fb618-1770-4edd-9c92-96428d1aa500") },
                { 100169, Guid.Parse("1f984696-75db-4f80-b43b-2db094bff068") },
                { 100377, Guid.Parse("59a7f02a-e0ac-4c7c-8da0-57207e96ebdd") },
                { 102803, Guid.Parse("658da0ec-702d-4bd2-8203-e1c8dcf22ac7") },
                { 100251, Guid.Parse("cffcbb2e-10c5-4a66-9583-eb82f1719737") },
                { 100214, Guid.Parse("8ff9369c-e8fb-4883-984a-f6770edf2dc4") },
                { 100625, Guid.Parse("e3b5ee87-f9d1-48e7-8644-361fc136a9dd") },
                { 100441, Guid.Parse("4459c0df-8859-45df-a082-82e1197e8f62") },
                { 100414, Guid.Parse("6e9235e6-23d3-4ed9-9962-a81cad44e9b5") },
                { 100438, Guid.Parse("5f30c1e9-5056-4026-a372-d27848c1fce2") },
                { 100254, Guid.Parse("4156a36e-b261-49c1-ab25-28a50eb40c6d") },
                { 102706, Guid.Parse("85a39191-bed7-40ba-9de6-2998dec50f0e") },
                { 100219, Guid.Parse("6b0ca428-6c2a-438d-a094-40b4ac2a7b9a") },
                { 101679, Guid.Parse("c25ae583-cc27-4aa0-b7e7-51f2627eb573") },
                { 101702, Guid.Parse("0b89058c-c5fc-4a8d-8608-872338995f27") },
                { 100740, Guid.Parse("1dbb3e97-16fe-4e3c-b397-7faa0b553aa6") },
                { 101675, Guid.Parse("850c9e3d-dc8c-46b6-adec-18421ac03351") },
                { 100567, Guid.Parse("202ffe5c-167a-48e4-94a5-da70f3bc305c") },
                { 100507, Guid.Parse("97dc45bd-8bce-4293-9d88-595e01efbd6d") },
                { 100506, Guid.Parse("631eee68-94a5-4d87-b78e-da321155741f") },
                { 100508, Guid.Parse("ccf7ec04-363c-42d4-ba27-df340a595893") },
                { 101794, Guid.Parse("f4b94717-03d6-4c55-90da-4f88d3c2d4e2") },
                { 100505, Guid.Parse("ac3d4f87-2312-4e0d-9725-cd7688562e83") },
                { 101693, Guid.Parse("d88b1e61-ac19-4172-8424-4cd473a9b90a") },
                { 100168, Guid.Parse("35ad0807-43c0-4e34-b31b-987e87022a58") },
                { 101657, Guid.Parse("39dd0fdf-6365-442f-ba66-9f4505467563") },
                { 100433, Guid.Parse("6444d20a-1450-42f6-9022-cda95c91c310") },
                { 101466, Guid.Parse("29459f16-416e-4d8b-b4f0-5144a5dd750d") },
                { 100430, Guid.Parse("1d197273-f749-4612-a2d8-d474ecdb6099") },
                { 100357, Guid.Parse("23cdf0ed-8f15-4eff-8404-2b677fa389ea") },
                { 100325, Guid.Parse("07d9f546-3712-4005-81cd-68dfbb0d5ac7") },
                { 100539, Guid.Parse("5147e22f-3b8f-4f73-a8b6-424fc977a77c") },
                { 100603, Guid.Parse("fba76d3d-16a3-4a42-bdf8-e4fdf381b006") },
                { 100601, Guid.Parse("d212b984-6604-4ef1-b64b-398c00fb6716") },
                { 100393, Guid.Parse("b96f27b8-d9b9-4a19-a17e-52fdbcd35870") },
                { 102998, Guid.Parse("455ed15f-4fb7-4b78-80b6-bff31c75791c") },
                { 100457, Guid.Parse("1238a4e2-deac-4be0-8581-ec1fe01d3a93") },
                { 100085, Guid.Parse("7dafbdf0-6fe1-410d-82d9-f2ee57e052c2") },
                { 100407, Guid.Parse("b08c9e88-fc82-487d-bcca-767a72aa7bed") },
                { 100396, Guid.Parse("624140db-7214-46c5-b449-dd7f8176a408") },
                { 100435, Guid.Parse("700aa6eb-b40a-41e1-bfa1-0966c089b4d3") },
                { 100712, Guid.Parse("dfe10ae9-a428-4f21-9a6d-4a9d880fac92") },
                { 102801, Guid.Parse("204f2d0c-bf54-4067-93a0-bdd28ff7f735") },
                { 100594, Guid.Parse("5c4155f2-d390-4fec-8d78-81ce7c38160d") },
                { 101655, Guid.Parse("ba6aa3ed-3d5a-4f49-adc7-ba9a783d7ed3") },
                { 100191, Guid.Parse("81ff58fb-47d6-4960-9889-c6b2e64a7201") },
                { 100413, Guid.Parse("152645c3-082f-4dd7-941d-602cefc14f61") },
                { 101884, Guid.Parse("ec465085-f395-49bb-b86e-954b15361009") },
                { 100587, Guid.Parse("8853839f-fce1-4fb4-b18d-4398f33a0b8d") },
                { 100207, Guid.Parse("4e56ee62-0b16-4792-a1f6-c7908f944aa6") },
                { 100772, Guid.Parse("9a8e511e-79c9-4544-a14e-06a981177dc9") },
                { 100773, Guid.Parse("77c939cf-2518-496c-ad30-fdabb97a2473") },
                { 100586, Guid.Parse("73265098-2e6c-4297-8f22-5427ddd8f7ff") },
                { 100113, Guid.Parse("cd0dcc10-8810-475c-b4eb-3e7839e389df") },
                { 100371, Guid.Parse("84a1aa12-3267-4821-baa0-123dc24e5192") },
                { 100538, Guid.Parse("72f60686-881c-49c3-b21a-f69d756201b7") },
                { 102250, Guid.Parse("aa6dfef2-0e84-4754-867d-295db5f80d9d") },
                { 100206, Guid.Parse("cfb80d6c-3a08-41ae-9b90-dbce9fa20f9e") },
                { 100735, Guid.Parse("83779681-1948-4b3d-be3c-b37d6be0b68f") },
                { 100572, Guid.Parse("0b6acdb7-6312-452d-99ec-a0fd33d0dabc") },
                { 100208, Guid.Parse("d39782d1-c111-44bf-b0c8-43838c0f2453") },
                { 101470, Guid.Parse("58983cc9-5e25-4caa-8337-fa46437048d4") },
                { 100055, Guid.Parse("a61ead94-4a61-4902-9735-923250bcfc55") },
                { 100054, Guid.Parse("572ccaee-c5ce-439c-bc5b-536b9f01740b") },
                { 100579, Guid.Parse("8a0582bd-180c-449f-9fc9-80cccff937af") },
                { 100751, Guid.Parse("6b7ca653-2ecb-4361-a985-54c26dcd5ae0") },
                { 102691, Guid.Parse("d863b4c5-b143-451d-b139-03ad40f4a7b1") },
                { 102695, Guid.Parse("6769dcfe-bc40-4843-b705-5713d5e94588") },
                { 101835, Guid.Parse("2b7b578f-0219-4002-999b-c9255f9c3241") },
                { 102773, Guid.Parse("bf130138-e57f-404e-afd2-8748f12cf94a") },
                { 100699, Guid.Parse("d7707ebd-f39d-4f5e-a2ca-5e9a4a971529") },
                { 100431, Guid.Parse("396effab-5f65-4730-aea6-6ad59bf2fc38") },
                { 100529, Guid.Parse("3c825bc4-f864-4170-8cdd-11107b301411") },
                { 100530, Guid.Parse("7abbbe07-b9db-4126-a0cc-f0588fef9574") },
                { 100812, Guid.Parse("229bc139-496e-4afc-b46c-5b051d538c4a") },
                { 100543, Guid.Parse("69c276f8-545e-431e-b5bb-be49dc5b8d96") },
                { 100511, Guid.Parse("79e67d6d-a16a-4bf0-899e-49799e9af055") },
                { 100727, Guid.Parse("fdb016c9-d458-4691-abd6-b18211470ee9") },
                { 100424, Guid.Parse("14b1554a-b890-4781-9b55-83317d61f5fc") },
                { 100062, Guid.Parse("beacce80-e39a-4c39-ad4c-b7ac2d0e5884") },
                { 101482, Guid.Parse("103aaba1-1e21-49f9-aa2b-17931dee4f65") },
                { 100607, Guid.Parse("dd54db99-fba0-4fe1-80c4-5fa7c6fed548") },
                { 100015, Guid.Parse("d24e0f97-bdd1-4f2b-a951-0d4cc371731d") },
                { 100491, Guid.Parse("f379c477-af4f-442f-99c9-a4ccdefafa77") },
                { 100155, Guid.Parse("c2e15dfc-7980-44ba-9348-4aa27125585b") },
                { 100488, Guid.Parse("19283fad-79e4-4e3a-93e1-47aaf77a67ce") },
                { 100490, Guid.Parse("af64c24b-bde7-4760-8a4e-8e5a4faa2c04") },
                { 100493, Guid.Parse("d8c1f97c-5975-4492-a614-4a47482fc44c") },
                { 100089, Guid.Parse("656754df-ebd0-42a5-812d-8c7009fefbdd") },
                { 100096, Guid.Parse("b67b0398-389a-490b-865b-1876936b4849") },
                { 100094, Guid.Parse("800f7002-4131-4cf3-be72-e7b11362d41c") },
                { 100321, Guid.Parse("5921bc93-df17-4e36-8c4e-26a053880f1c") },
                { 102795, Guid.Parse("a5882b43-6919-4f9b-b436-de93c436fc20") },
                { 100229, Guid.Parse("0f61b6d8-3a58-447d-8cb4-9cdc716949df") },
                { 100225, Guid.Parse("c50811b8-a36c-4d41-8a4b-ae55bded5be1") },
                { 100374, Guid.Parse("3ccba621-b083-4a14-9681-38ac3fbad132") },
                { 100233, Guid.Parse("4e0ab95e-ed78-42f1-8c93-931387b7fb91") },
                { 100789, Guid.Parse("09b72f49-f8d0-42c7-b8d7-1993aa38592c") },
                { 100392, Guid.Parse("08242285-32c9-4d5e-99a3-24c10bf0fa7d") },
                { 100227, Guid.Parse("2a493ecb-98e8-4036-8ff0-2243cdbc3ec2") },
                { 100235, Guid.Parse("efd26bbf-6d9f-4d57-97b2-2075f62835f6") },
                { 100234, Guid.Parse("fdd9bafc-16ed-456b-8dca-4957b277ac57") },
                { 100228, Guid.Parse("a259f128-48d9-4433-a6d6-33932fd43314") },
                { 100244, Guid.Parse("10e1033f-7831-46e3-840b-b71641b2daeb") },
                { 100231, Guid.Parse("2eb34766-9694-4e2a-bc96-ff6100a2e0af") },
                { 100376, Guid.Parse("e04102c0-9405-4766-9c04-44c30e65340d") },
                { 100769, Guid.Parse("5e62e60f-3697-4b8e-865b-170040a09578") },
                { 101342, Guid.Parse("eb7d93c9-690e-4721-ac7a-02a9e0080391") },
                { 101341, Guid.Parse("6c771286-4acc-40c1-9a2e-78d2a96abe18") },
                { 101662, Guid.Parse("e7f9a1d4-1a73-462b-89e1-c5aa1db0650a") },
                { 101661, Guid.Parse("a2a04cd6-0a7d-42a7-8020-0a62b9067f18") },
                { 100440, Guid.Parse("66a8eb15-e080-4a46-896a-358d754392d8") },
                { 100277, Guid.Parse("4a308364-baa2-4d16-8ce7-eee2201c71c3") },
                { 101343, Guid.Parse("f43e47db-7f84-4a3d-a09a-c43145ab2a1c") },
                { 101663, Guid.Parse("f64e03ad-88b9-488e-aa06-d436cdbfa755") },
                { 100447, Guid.Parse("7e0e8a36-bebd-4572-8d7d-6ea7fc3bd817") },
                { 100074, Guid.Parse("bcf75878-e4c5-4a1c-98ed-100e71715a53") },
                { 100060, Guid.Parse("30035eb8-a2a7-4022-8050-506cc759f63b") },
                { 100104, Guid.Parse("1d21477f-6146-4a4b-92d0-5a72d62e9187") },
                { 100397, Guid.Parse("305c2a03-4c23-42a7-9a4f-77bdf4191e56") },
                { 100611, Guid.Parse("9a55fb09-d8b3-4a9f-879b-718e35a26f38") },
                { 102733, Guid.Parse("63b6b363-60e3-4cab-98b7-baeab4d3d233") },
                { 100112, Guid.Parse("105c1050-1b30-4f18-a825-29ce176d9ade") },
                { 100038, Guid.Parse("27ed2474-031e-468a-8085-3b0dd79aac7d") },
                { 100340, Guid.Parse("d62ad812-c7ed-486a-97bc-a02f27b7ca48") },
                { 100394, Guid.Parse("3b49beef-307d-48bf-95ea-edcccf145c55") },
                { 100279, Guid.Parse("ecaa4718-f2d5-4266-9e1a-0b3a789705b2") },
                { 100351, Guid.Parse("90f01348-5275-4131-a95e-34a9bc483a47") },
                { 100037, Guid.Parse("72022f4e-31b2-47d0-b568-175d01d4cb0c") },
                { 100324, Guid.Parse("704366db-55bb-45b6-8cb3-a2580d4f8ed8") },
                { 100326, Guid.Parse("bd5cec11-1a9f-46bb-839d-3a343db2ae34") },
                { 100510, Guid.Parse("d58ed966-a5ef-4a02-9902-97f1db959699") },
                { 100344, Guid.Parse("2f14b5cd-ef03-4c99-835d-a8e47ad3cf4d") },
                { 100785, Guid.Parse("67e25b9e-b875-4fda-8bf4-12921898d24c") },
                { 101971, Guid.Parse("b6849a86-43c4-4a77-9fa7-e2713d236c8b") },
                { 100602, Guid.Parse("d18b32ec-cd3e-41fd-b805-e0342c47307e") },
                { 100633, Guid.Parse("264c79f8-d6e7-43bd-83c9-b484ef6719ce") },
                { 102807, Guid.Parse("a244c49d-c2b6-438c-aec4-de2780d56c69") },
                { 102922, Guid.Parse("9c83c29e-6e65-4111-82cc-2c89d5978c69") },
                { 100545, Guid.Parse("78e60851-7545-4809-9b29-50dc6eb2ba65") },
                { 100815, Guid.Parse("ca1648f2-6c90-4cd1-94a6-728508e06975") },
                { 100434, Guid.Parse("16a9e81c-5f6b-4287-bfe4-97cac1f32393") },
                { 100455, Guid.Parse("25cb2d4b-5287-4a9f-8614-c7714a23df6f") },
                { 100406, Guid.Parse("5800d7a8-a41d-4bab-b7a7-f11a06010716") },
                { 100197, Guid.Parse("2d01a147-7cfb-4431-a4f6-4643c82cb0f2") },
                { 100199, Guid.Parse("82b196a2-fb8b-423c-b92c-ecc3f782098d") },
                { 102694, Guid.Parse("220ccd19-e744-4ab4-adea-5754edd5f96e") },
                { 100691, Guid.Parse("bb616d54-dfa0-4e61-8d81-42c578887b86") },
                { 100635, Guid.Parse("e5cf7bd6-c997-460a-9666-9cebe3fa5dd0") },
                { 100571, Guid.Parse("3fca533f-731a-4d27-9ca9-ae2fcde1336a") },
                { 100318, Guid.Parse("a1dbec9e-80a4-4277-b83a-be57e5552d5b") },
                { 100521, Guid.Parse("16478017-b8c9-4ead-8469-c742b6477693") },
                { 100152, Guid.Parse("248c2932-2034-43e1-9d66-c66cd2ad9f6f") },
                { 100419, Guid.Parse("2980adcf-d54b-41c0-85ae-b7b93408bfb7") },
                { 100417, Guid.Parse("0f2e2c52-ca98-4961-9ea9-3b01c639ece6") },
                { 100673, Guid.Parse("25ec8ee2-1fce-4eb9-b1e7-0901d18ffd22") },
                { 100338, Guid.Parse("9907384a-90b3-4559-b7d9-f977865bd461") },
                { 100342, Guid.Parse("13120958-20c0-450f-b1a1-e60633a0a25d") },
                { 100762, Guid.Parse("7107471e-187b-4461-9220-abcd350e28ce") },
                { 100618, Guid.Parse("eb73cf0d-5ed9-4f74-a60e-3fef24917c62") },
                { 104163, Guid.Parse("ed8f0c00-5d46-480f-a968-80b995205178") },
                { 100615, Guid.Parse("9612000e-2a15-4f5a-a0ba-fcf9f07dcc71") },
                { 100353, Guid.Parse("84ae7906-9d02-47a6-aaf6-7a50a1392005") },
                { 100331, Guid.Parse("19a9e700-b0cd-4e70-af08-1eac30e84955") },
                { 100577, Guid.Parse("54e66c6d-fdd9-498f-8878-2f30a78acf6c") },
                { 100258, Guid.Parse("9ae9dbf8-3128-4604-aa2e-8b84a14e0cd4") },
                { 102235, Guid.Parse("d8d0a1f0-f0b8-4c25-bf70-3867f9e883f5") },
                { 100298, Guid.Parse("822969f0-2b65-44bf-8779-470af03c518b") },
                { 100100, Guid.Parse("30be9f41-1fd0-451f-bea0-0efc07f6e6cd") },
                { 100099, Guid.Parse("7b19c99f-d03b-4e3c-94d6-f09eb871c1b2") },
                { 100352, Guid.Parse("c95cee84-57ce-4b04-af19-29420aa6c73a") },
                { 100766, Guid.Parse("e53bc8ed-ac1d-4cec-ba1b-782dcb2379be") },
                { 100040, Guid.Parse("19a25fbf-641c-4944-81fc-64f7a1ff8209") },
                { 102800, Guid.Parse("168da60b-3d7c-4c20-a8f8-ae4b98bd40fd") },
                { 100573, Guid.Parse("e409ee78-b9d6-4e00-abf0-b57c3e37ada4") },
                { 100359, Guid.Parse("d8e15269-e6ad-450b-ae28-754e38d61cb1") },
                { 100133, Guid.Parse("97902e64-5cdd-42da-bd62-e0e051d5bbff") },
                { 104157, Guid.Parse("ae8eca18-9a10-4b1d-8d3e-58a1fa4afc3b") },
                { 100129, Guid.Parse("0f2d173e-0cf3-447e-af00-76bef4a545cf") },
                { 100590, Guid.Parse("50409b12-4e17-436e-a71f-727153e8db6d") },
                { 100209, Guid.Parse("9acb67f7-5065-4275-9332-18ae035ac1cf") },
                { 102794, Guid.Parse("2e44a274-bf0d-4ae9-9cd5-5259707fde98") },
                { 101767, Guid.Parse("6452c062-3e15-4b25-9f90-744a611f82e2") },
                { 100468, Guid.Parse("8f0591e3-bf76-48db-a5b6-20de16fb4205") },
                { 101442, Guid.Parse("2bec6cf7-ec07-413a-a5e1-00cb8eba4aa0") },
                { 101786, Guid.Parse("b6a9f82a-7c66-4146-8c28-35a488a23deb") },
                { 102964, Guid.Parse("7f3f92d5-800e-4204-b82d-70556343d71f") },
                { 100034, Guid.Parse("72ed4f46-e843-4562-a2cc-5c123980bea2") },
                { 100578, Guid.Parse("a8e5be91-2b13-40b1-8cd5-8ab930156608") },
                { 100807, Guid.Parse("8cd60338-df94-4bc3-8ec8-3e9b76b8c3d5") },
                { 100347, Guid.Parse("009280f3-7a91-4d05-a84b-03c0ea8d567d") },
                { 100095, Guid.Parse("0d1a15f8-1b8c-4b08-9920-54a7e3a658ef") },
                { 101763, Guid.Parse("b341f359-90d2-40b3-9151-e62ee810cdcd") },
                { 101760, Guid.Parse("02dc97c6-c88f-41d7-920f-5da3efcf704c") },
                { 100216, Guid.Parse("8d8486f8-f798-49e7-8605-4bcf76d00545") },
                { 100250, Guid.Parse("48970c6b-3730-43ee-9de8-4f52d66b3133") },
                { 100717, Guid.Parse("f0886e10-ea44-4777-ba49-cd450fefe44b") },
                { 100261, Guid.Parse("55013979-22fb-45ac-91ff-39be78e94cd9") },
                { 100253, Guid.Parse("a6ee258e-6961-4a8d-bdd2-b31f2813d42b") },
                { 102804, Guid.Parse("51160178-5372-44e6-8799-1de3f2618e79") },
                { 102208, Guid.Parse("e0d68c4d-4e32-44b4-bee2-7617f085b0f6") },
                { 100031, Guid.Parse("1ff2fb84-f563-4bcf-814e-8a7c5c4b45ea") },
                { 100033, Guid.Parse("fde57802-5a0a-4a4d-8bc1-2f71a839553e") },
                { 100032, Guid.Parse("a43a59c8-386d-4353-b05a-fdcfa3e228bd") },
                { 100030, Guid.Parse("a33124fb-cedf-45ce-9878-38d54b6c0b15") },
                { 100036, Guid.Parse("99cc7c27-dbf8-4c09-89d7-11ce0f8dbc32") },
                { 100035, Guid.Parse("a5e07096-6a73-411a-b309-6856220ca9fa") },
                { 100385, Guid.Parse("7d67964a-9d8b-4fe1-9bc9-d90ec1730cc9") },
                { 100077, Guid.Parse("468fe5f5-effc-42ea-a4f4-76e9e96b476a") },
                { 103436, Guid.Parse("e8f5e40b-55ae-480a-b638-1abcb5857b2e") },
                { 100267, Guid.Parse("37c1a316-8dba-41b8-89f5-d3ff03ce870e") },
                { 102076, Guid.Parse("a1c6e871-6767-493d-a9e8-78c9be997239") },
                { 100315, Guid.Parse("0ea54717-8799-484f-ac3f-b7614385bd94") },
                { 100137, Guid.Parse("eed6fa47-21c5-467b-a671-c44cc361314b") },
                { 100362, Guid.Parse("90f81574-e863-4a88-9a42-7f0641981a55") },
                { 100479, Guid.Parse("ad6c551d-b1fc-4a93-9c0f-d376214ea823") },
                { 100776, Guid.Parse("d6517747-3b40-45ec-a2b7-4c54a5ec2f9a") },
                { 101808, Guid.Parse("7dfd0df0-315b-4ebc-919b-f8a9b3d7ac86") },
                { 100779, Guid.Parse("118db76e-fa64-49d4-bc7e-c8d47f2353b9") },
                { 102693, Guid.Parse("838b200c-8075-49f0-9940-f7f3b1d83ed5") },
                { 100025, Guid.Parse("ab8a287c-b6bb-423f-9bd7-2dfcc2dfcde8") },
                { 102671, Guid.Parse("09ea07b6-0b18-4392-84fb-d60c682b3984") },
                { 100808, Guid.Parse("3c79e720-342a-472d-b963-920f1be94042") },
                { 100591, Guid.Parse("16d7f788-25e5-474d-b12a-ee9a5edc5ded") },
                { 100346, Guid.Parse("2d85c996-a476-4a0e-a66c-481b26758366") },
                { 100263, Guid.Parse("431cc832-82ac-4cb5-a562-d5faa4c06de7") },
                { 100840, Guid.Parse("21b777c4-b0cd-49a6-9f0b-f06f370ef357") },
                { 101728, Guid.Parse("33ae1406-23ba-4f11-8dc2-24dd7e15d55b") },
                { 101670, Guid.Parse("a44c2d67-f8e0-47c8-b5cd-7625f0346263") },
                { 100463, Guid.Parse("e97f8921-b6d2-479e-ba9d-27e0b4030dab") },
                { 101421, Guid.Parse("a7ca7faa-9164-4fcd-aa2f-b6d061a402f9") },
                { 100291, Guid.Parse("98e6b75b-f526-49d1-b62f-4c32147f730c") },
                { 101452, Guid.Parse("176ef6bf-0d71-4fe4-80b8-5edb24692ed0") },
                { 100323, Guid.Parse("3c7065f0-72f0-4c8a-ae41-03ccfeb595f9") },
                { 100237, Guid.Parse("c11b8863-82ca-43aa-b699-3f9df1938cb4") },
                { 100617, Guid.Parse("bf49663d-44c0-4ec7-9048-e101025f75e5") },
                { 100686, Guid.Parse("084907b7-6481-418b-9c79-da226a12a542") },
                { 100885, Guid.Parse("82967381-aaa1-4b26-a856-e1b0ae970a49") },
                { 100411, Guid.Parse("1cb9627d-55ec-4513-9984-b372992e13a5") },
                { 100690, Guid.Parse("f47198b0-6a1d-4103-93b1-2327d0570585") },
                { 100581, Guid.Parse("6f90ad3f-4d25-4c9b-b6d5-17f690018e85") },
                { 100211, Guid.Parse("8a733676-ceb7-4187-8e7e-abd96b963258") },
                { 100198, Guid.Parse("20118ac8-98be-4c2c-924b-fc029a18d091") },
                { 100524, Guid.Parse("8a0b13f8-1262-47f5-a91f-48d8165651f0") },
                { 100803, Guid.Parse("ac8deec1-afe5-40d1-a418-f456dcc1c7c0") },
                { 101833, Guid.Parse("28c8ad31-5eab-4a9d-8900-2bbbe0680a35") },
                { 100425, Guid.Parse("9043e3dd-2495-4d22-ad45-ec143d89c439") },
                { 100360, Guid.Parse("2186fd4c-ba71-4e44-8b9c-da514b7d45a2") },
                { 100777, Guid.Parse("d2996e87-18f9-4c89-b837-7f45cca572cc") },
                { 100399, Guid.Parse("b7d992bc-cfd3-41bb-a62e-53dd40c3c2d2") },
                { 100754, Guid.Parse("2e925d9d-8410-4e66-9fa8-e7d2302c75c4") },
                { 100726, Guid.Parse("c6c5b2ce-b070-41ca-a21d-30a6a9840dfb") }
            };
        }
    }
}