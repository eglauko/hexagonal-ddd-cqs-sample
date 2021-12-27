
namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Interface para entidades que possuem um campo do tipo Guid.
/// </para>
/// <para>
///     Este campo de identificador único não é o mesmo que o ID.
/// </para>
/// <para>
///     O objetivo desta propriedade Guid é manter um identificador global único de uma mesma entidade
///     distribuída por várias bases de dados de contextos diferentes.
///     Cada base de dados poderá ter sua estrutura e seu tipo de Id, com valores diferentes para estes IDs,
///     porém, este Guid se repetirá por entre todas as bases de dados. 
/// </para>
/// </summary>
public interface IHasGuid
{
    /// <summary>
    /// O Guid da entidade.
    /// </summary>
    Guid Guid { get; }
}