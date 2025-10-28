import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { gameService } from '../services/gameService';
import { commentService } from '../services/commentService';
import { scoreService } from '../services/scoreService';
import { useAuth } from '../contexts/AuthContext';
import type { Game, Comment, GameScore } from '../types';
import './GameDetail.css';

const GameDetail: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const [game, setGame] = useState<Game | null>(null);
  const [comments, setComments] = useState<Comment[]>([]);
  const [leaderboard, setLeaderboard] = useState<GameScore[]>([]);
  const [loading, setLoading] = useState(true);
  const [newComment, setNewComment] = useState('');
  const [rating, setRating] = useState(5);
  const { isAuthenticated } = useAuth();

  useEffect(() => {
    const fetchData = async () => {
      if (!slug) return;
      
      try {
        const gameData = await gameService.getGameBySlug(slug);
        setGame(gameData);
        
        const [commentsData, leaderboardData] = await Promise.all([
          commentService.getGameComments(gameData.id),
          scoreService.getGameLeaderboard(gameData.id, 10),
        ]);
        
        setComments(commentsData);
        setLeaderboard(leaderboardData);
        
        // Track play
        await gameService.trackPlay(gameData.id, {
          gameId: gameData.id,
          deviceType: /Mobile/.test(navigator.userAgent) ? 'mobile' : 'desktop',
          browser: navigator.userAgent,
        });
      } catch (error) {
        console.error('Error fetching game:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [slug]);

  const handleCommentSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!game || !newComment.trim()) return;

    try {
      const comment = await commentService.createComment({
        gameId: game.id,
        content: newComment,
        rating,
      });
      setComments([comment, ...comments]);
      setNewComment('');
      setRating(5);
    } catch (error) {
      console.error('Error submitting comment:', error);
    }
  };

  if (loading) {
    return <div className="loading">Loading...</div>;
  }

  if (!game) {
    return <div className="error">Game not found</div>;
  }

  return (
    <div className="game-detail">
      <div className="container">
        <div className="game-header">
          <h1>{game.title}</h1>
          <p className="game-meta">
            Category: {game.categoryName} | ⭐ {game.averageRating.toFixed(1)} ({game.ratingCount} ratings)
          </p>
        </div>

        <div className="game-container">
          <div className="game-frame">
            <iframe
              src={game.gameUrl}
              title={game.title}
              width={game.width}
              height={game.height}
              frameBorder="0"
              allowFullScreen
            ></iframe>
          </div>
          
          <div className="game-info">
            <h2>About this game</h2>
            <p>{game.description}</p>
            {game.developer && <p><strong>Developer:</strong> {game.developer}</p>}
            {game.tags && <p><strong>Tags:</strong> {game.tags}</p>}
            <div className="game-actions">
              <button className="btn-share">Share</button>
            </div>
          </div>
        </div>

        <div className="game-sections">
          <div className="leaderboard-section">
            <h2>Leaderboard</h2>
            {leaderboard.length > 0 ? (
              <table className="leaderboard-table">
                <thead>
                  <tr>
                    <th>Rank</th>
                    <th>Player</th>
                    <th>Score</th>
                  </tr>
                </thead>
                <tbody>
                  {leaderboard.map((score, index) => (
                    <tr key={score.id}>
                      <td>{index + 1}</td>
                      <td>{score.playerName || 'Anonymous'}</td>
                      <td>{score.score}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            ) : (
              <p>No scores yet. Be the first to play!</p>
            )}
          </div>

          <div className="comments-section">
            <h2>Comments & Ratings</h2>
            
            {isAuthenticated ? (
              <form onSubmit={handleCommentSubmit} className="comment-form">
                <div className="form-group">
                  <label>Rating: {rating} / 5</label>
                  <input
                    type="range"
                    min="1"
                    max="5"
                    value={rating}
                    onChange={(e) => setRating(Number(e.target.value))}
                  />
                </div>
                <textarea
                  placeholder="Write your comment..."
                  value={newComment}
                  onChange={(e) => setNewComment(e.target.value)}
                  required
                ></textarea>
                <button type="submit" className="btn-primary">Submit Comment</button>
              </form>
            ) : (
              <p>Please login to leave a comment.</p>
            )}

            <div className="comments-list">
              {comments.map((comment) => (
                <div key={comment.id} className="comment">
                  <div className="comment-header">
                    <strong>{comment.username}</strong>
                    {comment.rating > 0 && <span className="comment-rating">⭐ {comment.rating}</span>}
                    <span className="comment-date">
                      {new Date(comment.createdAt).toLocaleDateString()}
                    </span>
                  </div>
                  <p>{comment.content}</p>
                </div>
              ))}
            </div>
          </div>
        </div>

        <div className="ad-section">
          <div className="ad-placeholder">
            <p>Advertisement Space (Google AdSense / Yandex)</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GameDetail;
