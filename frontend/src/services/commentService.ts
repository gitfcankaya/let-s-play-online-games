import api from './api';
import type { Comment, CreateCommentDto } from '../types';

export const commentService = {
  async getGameComments(gameId: number): Promise<Comment[]> {
    const response = await api.get(`/comments/game/${gameId}`);
    return response.data;
  },

  async createComment(commentData: CreateCommentDto): Promise<Comment> {
    const response = await api.post('/comments', commentData);
    return response.data;
  },

  async deleteComment(commentId: number): Promise<void> {
    await api.delete(`/comments/${commentId}`);
  },
};
