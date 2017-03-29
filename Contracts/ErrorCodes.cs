namespace Contracts
{
    /// <summary>
    ///  Gibt diverse mögliche Ablauffälle für Vorgänge an, welche für die grafische Darstellung wichtig sind.
    /// </summary>
    public class ErrorCodes
    {
        public enum BuyAktie
        {
            NoError = 1000,
            NotEnoughMoney = 2000
        }

        public enum Login
        {
            NoError = 1000,
            NicknameNotFound = 2000,
            WrongPassword = 3000
        }

        public enum Register
        {
            NoError = 1000,
            NameAlreadyTaken = 2000
        }
        public enum SellAktie
        {
            NoError = 1000,
            NotEnoughAmount = 2000
        }
    }
}
