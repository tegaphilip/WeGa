﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeGa.GameServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServiceReference.IGameService")]
    public interface IGameService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Login", ReplyAction="http://tempuri.org/IGameService/LoginResponse")]
        System.Collections.Generic.Dictionary<string, string> Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Login", ReplyAction="http://tempuri.org/IGameService/LoginResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/RegisterPlayer", ReplyAction="http://tempuri.org/IGameService/RegisterPlayerResponse")]
        bool RegisterPlayer(string username, string nickname, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/RegisterPlayer", ReplyAction="http://tempuri.org/IGameService/RegisterPlayerResponse")]
        System.Threading.Tasks.Task<bool> RegisterPlayerAsync(string username, string nickname, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetPlayerNicknames", ReplyAction="http://tempuri.org/IGameService/GetPlayerNicknamesResponse")]
        string[] GetPlayerNicknames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetPlayerNicknames", ReplyAction="http://tempuri.org/IGameService/GetPlayerNicknamesResponse")]
        System.Threading.Tasks.Task<string[]> GetPlayerNicknamesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/CreateGame", ReplyAction="http://tempuri.org/IGameService/CreateGameResponse")]
        System.Collections.Generic.Dictionary<string, string> CreateGame(string SenderNickName, string ReceiverNickName, string Letters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/CreateGame", ReplyAction="http://tempuri.org/IGameService/CreateGameResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> CreateGameAsync(string SenderNickName, string ReceiverNickName, string Letters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/SendWord", ReplyAction="http://tempuri.org/IGameService/SendWordResponse")]
        System.Collections.Generic.Dictionary<string, string> SendWord(string wordPlayed, int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/SendWord", ReplyAction="http://tempuri.org/IGameService/SendWordResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> SendWordAsync(string wordPlayed, int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetGameRequests", ReplyAction="http://tempuri.org/IGameService/GetGameRequestsResponse")]
        System.Collections.Generic.Dictionary<string, string>[] GetGameRequests(string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetGameRequests", ReplyAction="http://tempuri.org/IGameService/GetGameRequestsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>[]> GetGameRequestsAsync(string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetGameWordsHistory", ReplyAction="http://tempuri.org/IGameService/GetGameWordsHistoryResponse")]
        System.Collections.Generic.Dictionary<string, string>[] GetGameWordsHistory(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetGameWordsHistory", ReplyAction="http://tempuri.org/IGameService/GetGameWordsHistoryResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>[]> GetGameWordsHistoryAsync(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/EndGame", ReplyAction="http://tempuri.org/IGameService/EndGameResponse")]
        System.Collections.Generic.Dictionary<string, string> EndGame(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/EndGame", ReplyAction="http://tempuri.org/IGameService/EndGameResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> EndGameAsync(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/NeglectGame", ReplyAction="http://tempuri.org/IGameService/NeglectGameResponse")]
        System.Collections.Generic.Dictionary<string, string> NeglectGame(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/NeglectGame", ReplyAction="http://tempuri.org/IGameService/NeglectGameResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> NeglectGameAsync(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ResignGame", ReplyAction="http://tempuri.org/IGameService/ResignGameResponse")]
        System.Collections.Generic.Dictionary<string, string> ResignGame(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ResignGame", ReplyAction="http://tempuri.org/IGameService/ResignGameResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> ResignGameAsync(int gameId, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetLeaderBoard", ReplyAction="http://tempuri.org/IGameService/GetLeaderBoardResponse")]
        System.Collections.Generic.Dictionary<string, double> GetLeaderBoard(string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetLeaderBoard", ReplyAction="http://tempuri.org/IGameService/GetLeaderBoardResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, double>> GetLeaderBoardAsync(string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/PingDatabase", ReplyAction="http://tempuri.org/IGameService/PingDatabaseResponse")]
        bool PingDatabase();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/PingDatabase", ReplyAction="http://tempuri.org/IGameService/PingDatabaseResponse")]
        System.Threading.Tasks.Task<bool> PingDatabaseAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceChannel : WeGa.GameServiceReference.IGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameServiceClient : System.ServiceModel.ClientBase<WeGa.GameServiceReference.IGameService>, WeGa.GameServiceReference.IGameService {
        
        public GameServiceClient() {
        }
        
        public GameServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GameServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.Dictionary<string, string> Login(string username, string password) {
            return base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
        
        public bool RegisterPlayer(string username, string nickname, string password) {
            return base.Channel.RegisterPlayer(username, nickname, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterPlayerAsync(string username, string nickname, string password) {
            return base.Channel.RegisterPlayerAsync(username, nickname, password);
        }
        
        public string[] GetPlayerNicknames() {
            return base.Channel.GetPlayerNicknames();
        }
        
        public System.Threading.Tasks.Task<string[]> GetPlayerNicknamesAsync() {
            return base.Channel.GetPlayerNicknamesAsync();
        }
        
        public System.Collections.Generic.Dictionary<string, string> CreateGame(string SenderNickName, string ReceiverNickName, string Letters) {
            return base.Channel.CreateGame(SenderNickName, ReceiverNickName, Letters);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> CreateGameAsync(string SenderNickName, string ReceiverNickName, string Letters) {
            return base.Channel.CreateGameAsync(SenderNickName, ReceiverNickName, Letters);
        }
        
        public System.Collections.Generic.Dictionary<string, string> SendWord(string wordPlayed, int gameId, string nickname) {
            return base.Channel.SendWord(wordPlayed, gameId, nickname);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> SendWordAsync(string wordPlayed, int gameId, string nickname) {
            return base.Channel.SendWordAsync(wordPlayed, gameId, nickname);
        }
        
        public System.Collections.Generic.Dictionary<string, string>[] GetGameRequests(string nickname) {
            return base.Channel.GetGameRequests(nickname);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>[]> GetGameRequestsAsync(string nickname) {
            return base.Channel.GetGameRequestsAsync(nickname);
        }
        
        public System.Collections.Generic.Dictionary<string, string>[] GetGameWordsHistory(int gameId, string nickname) {
            return base.Channel.GetGameWordsHistory(gameId, nickname);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>[]> GetGameWordsHistoryAsync(int gameId, string nickname) {
            return base.Channel.GetGameWordsHistoryAsync(gameId, nickname);
        }
        
        public System.Collections.Generic.Dictionary<string, string> EndGame(int gameId, string nickname) {
            return base.Channel.EndGame(gameId, nickname);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> EndGameAsync(int gameId, string nickname) {
            return base.Channel.EndGameAsync(gameId, nickname);
        }
        
        public System.Collections.Generic.Dictionary<string, string> NeglectGame(int gameId, string nickname) {
            return base.Channel.NeglectGame(gameId, nickname);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> NeglectGameAsync(int gameId, string nickname) {
            return base.Channel.NeglectGameAsync(gameId, nickname);
        }
        
        public System.Collections.Generic.Dictionary<string, string> ResignGame(int gameId, string nickname) {
            return base.Channel.ResignGame(gameId, nickname);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> ResignGameAsync(int gameId, string nickname) {
            return base.Channel.ResignGameAsync(gameId, nickname);
        }
        
        public System.Collections.Generic.Dictionary<string, double> GetLeaderBoard(string type) {
            return base.Channel.GetLeaderBoard(type);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, double>> GetLeaderBoardAsync(string type) {
            return base.Channel.GetLeaderBoardAsync(type);
        }
        
        public bool PingDatabase() {
            return base.Channel.PingDatabase();
        }
        
        public System.Threading.Tasks.Task<bool> PingDatabaseAsync() {
            return base.Channel.PingDatabaseAsync();
        }
    }
}
