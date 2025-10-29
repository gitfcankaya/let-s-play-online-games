export interface User {
  id: number;
  username: string;
  email: string;
  fullName?: string;
  country?: string;
  isAdmin: boolean;
}

export interface Game {
  id: number;
  title: string;
  slug: string;
  description: string;
  thumbnailUrl: string;
  fullImageUrl?: string;
  gameUrl: string;
  gameType: GameType;
  categoryId: number;
  categoryName: string;
  isActive: boolean;
  isFeatured: boolean;
  viewCount: number;
  playCount: number;
  averageRating: number;
  ratingCount: number;
  developer?: string;
  tags?: string;
  ageRating: AgeRating;
  width: number;
  height: number;
  isMobileCompatible: boolean;
  createdAt: string;
}

export enum GameType {
  Html5 = 0,
  Flash = 1,
  Unity = 2,
  Iframe = 3,
  External = 4
}

export enum AgeRating {
  Everyone = 0,
  Teen = 1,
  Mature = 2
}

export interface Category {
  id: number;
  name: string;
  slug: string;
  description?: string;
  iconUrl?: string;
  displayOrder: number;
  gamesCount: number;
}

export interface Comment {
  id: number;
  gameId: number;
  userId: number;
  username: string;
  content: string;
  rating: number;
  parentCommentId?: number;
  likesCount: number;
  createdAt: string;
  replies: Comment[];
}

export interface GameScore {
  id: number;
  gameId: number;
  userId: number;
  playerName?: string;
  score: number;
  country?: string;
  createdAt: string;
}

export interface AuthResponse {
  token: string;
  user: User;
}

export interface RegisterDto {
  username: string;
  email: string;
  password: string;
  fullName?: string;
  dateOfBirth?: string;
  country?: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface CreateCommentDto {
  gameId: number;
  content: string;
  rating: number;
  parentCommentId?: number;
}

export interface CreateGameScoreDto {
  gameId: number;
  score: number;
  playerName?: string;
}

export interface CreateGameDto {
  title: string;
  slug: string;
  description: string;
  thumbnailUrl: string;
  fullImageUrl?: string;
  gameUrl: string;
  gameType: GameType;
  categoryId: number;
  isActive: boolean;
  isFeatured: boolean;
  developer?: string;
  tags?: string;
  ageRating: AgeRating;
  width: number;
  height: number;
  isMobileCompatible: boolean;
}
