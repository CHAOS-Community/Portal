CREATE PROCEDURE `Ticket_Create`(
    IN  GUID            BINARY(16),
    IN  TicketTypeID    INTEGER,
    IN  XML             TEXT,
    IN  Callback        VARCHAR(2048)
)
BEGIN

    INSERT INTO Ticket( GUID, TicketTypeID, XML, Callback, DateCreated, DateUsed )
         VALUES ( GUID, TicketTypeID, XML, Callback, NOW(), null );

END