CREATE PROCEDURE `Ticket_Get`(
    IN  GUID    BINARY(16)
)
BEGIN

    SELECT	Ticket.*
      FROM	Ticket
     WHERE	Ticket.GUID = GUID;

END