namespace Rekrutacja.Client.Models.Dtos.Contacts;
public record ContactListDto(List<ContactDto> Contacts, int PageSize, int PageNumber, int Count);