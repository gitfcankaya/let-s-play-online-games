import React from 'react';
import { Link } from 'react-router-dom';
import type { Game } from '../../types';
import './GameCard.css';

interface GameCardProps {
  game: Game;
}

const GameCard: React.FC<GameCardProps> = ({ game }) => {
  return (
    <Link to={`/game/${game.slug}`} className="game-card">
      <div className="game-card-image">
        <img src={game.thumbnailUrl} alt={game.title} />
        {game.isFeatured && <span className="featured-badge">Featured</span>}
      </div>
      <div className="game-card-content">
        <h3>{game.title}</h3>
        <p className="game-category">{game.categoryName}</p>
        <div className="game-stats">
          <span>⭐ {game.averageRating.toFixed(1)}</span>
          <span>👁️ {game.viewCount}</span>
          <span>🎮 {game.playCount}</span>
        </div>
      </div>
    </Link>
  );
};

export default GameCard;
