import React, { useState, useEffect } from "react";
import Post from './Post';
import { getAllPosts, getBySearch, searchPost } from "../modules/PostManager";

const PostList = () => {
  const [posts, setPosts] = useState([]);
  const [query, setQuery] = useState("");

  const getPosts = () => {
    getAllPosts().then(allPosts => setPosts(allPosts));
  };

  const searchPosts = () => {
    getBySearch(query).then(posts => setPosts(posts));
  };

  useEffect(() => {
    getPosts();
  }, []);

  return (
    <>
    <div className="search-bar">
      <input property="search" onChange={e => setQuery(e.target.value)} /><br/>
      <button onClick={searchPosts}>Search</button>
    </div>
    <div className="container">
      <div className="row justify-content-center">
        <div className="cards-column">
          {posts.map((post) => (
            <Post key={post.id} post={post} />
          ))}
        </div>
      </div>
    </div>
    </>
  );
};

export default PostList;
