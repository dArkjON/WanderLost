﻿using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WanderLost.Shared;

namespace WanderLost.Client
{
    public class ClientStaticDataController
    {
        public bool Initialized { get; private set; }
        public Dictionary<string, ServerRegion> ServerRegions { get; private set; } = new();
        public Dictionary<string, MerchantData> Merchants { get; private set; } = new();
        public List<Zone> Zones { get; private set; } = new();

        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;

        public ClientStaticDataController(NavigationManager navigationManager, HttpClient httpClient)
        {
            _navigationManager = navigationManager;
            _httpClient = httpClient;
        }

        public async Task Init()
        {
            if (Initialized) return;

            Merchants = await _httpClient.GetFromJsonAsync<Dictionary<string, MerchantData>>(_navigationManager.ToAbsoluteUri("/data/merchants.json"), Utils.JsonOptions) ?? new();
            ServerRegions = await _httpClient.GetFromJsonAsync<Dictionary<string, ServerRegion>>(_navigationManager.ToAbsoluteUri("/data/servers.json"), Utils.JsonOptions) ?? new();
            Zones = await _httpClient.GetFromJsonAsync<List<Zone>>(_navigationManager.ToAbsoluteUri("/data/zones.json"), Utils.JsonOptions) ?? new();

            Initialized = true;
        }
    }
}
