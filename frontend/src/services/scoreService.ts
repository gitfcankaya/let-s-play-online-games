import api from './api';
import type { GameScore, CreateGameScoreDto } from '../types';

export const scoreService = {
  async getGameLeaderboard(gameId: number, limit: number = 100): Promise<GameScore[]> {
    const response = await api.get(`/scores/game/${gameId}?limit=${limit}`);
    return response.data;
  },

  async submitScore(scoreData: CreateGameScoreDto): Promise<GameScore> {
    const response = await api.post('/scores', scoreData);
    return response.data;
  },

  async getMyBestScore(gameId: number): Promise<number> {
    const response = await api.get(`/scores/game/${gameId}/my-best`);
    return response.data.score;
  },
};
