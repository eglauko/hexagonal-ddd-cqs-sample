namespace HexaSamples.Domain.OrdemAggregate;

public enum OrdemSituacao
{
    EmEdicao,
    Fechada,
    Cancelada,

    // as situações daqui em diante seriam opcionais,
    // é uma questão de design para atender as regras de negócio.
    // se poderia deixar as opções adiante para outras entidades,
    // como de processo de finalização da ordem e processo de entrega.
    // Estas outras entidades, que são de contexto diferentes,
    // poderiam ter suas situações, mas ainda poderia ser replicada a
    // situação para a ordem.
    // É uma decisão de design que seria tomada dependendo das regras de negócio.

    Reservada,
    Paga,
    Despachada,
    Entregue,
    Retornada,
    Finalizada
}