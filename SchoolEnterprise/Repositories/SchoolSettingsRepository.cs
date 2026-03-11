using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class SchoolSettingsRepository(XmlStorageService xml) : BaseXmlRepository<SchoolSettings>(xml, "school-settings.xml")
{
    public SchoolSettings? Get() => Read().FirstOrDefault();
    public void Save(SchoolSettings entity) => Write([entity]);
}
