import api from './api';
import type { Game, CreateGameDto } from '../types';

export const gameService = {
  async getAllGames(categoryId?: number, isFeatured?: boolean): Promise<Game[]> {
    const params = new URLSearchParams();
    if (categoryId) params.append('categoryId', categoryId.toString());
    if (isFeatured !== undefined) params.append('isFeatured', isFeatured.toString());
    
    const response = await api.get(`/games?${params.toString()}`);
    return response.data;
  },

  async getGameById(id: number): Promise<Game> {
    const response = await api.get(`/games/${id}`);
    return response.data;
  },

  async getGameBySlug(slug: string): Promise<Game> {
    const response = await api.get(`/games/slug/${slug}`);
    return response.data;
  },

  async searchGames(query: string): Promise<Game[]> {
    const response = await api.get(`/games/search?query=${encodeURIComponent(query)}`);
    return response.data;
  },

  async createGame(gameData: CreateGameDto): Promise<Game> {
    const response = await api.post('/games', gameData);
    return response.data;
  },

  async updateGame(id: number, gameData: CreateGameDto): Promise<Game> {
    const response = await api.put(`/games/${id}`, { ...gameData, id });
    return response.data;
  },

  async deleteGame(id: number): Promise<void> {
    await api.delete(`/games/${id}`);
  },

  async trackPlay(gameId: number, trackData: any): Promise<void> {
    await api.post(`/games/${gameId}/play`, trackData);
  },
};
