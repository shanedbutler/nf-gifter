import React, { useState, useEffect } from "react";
import Post from './Post';
import { getPost } from "../modules/PostManager";
import { useParams } from "react-router-dom";

// TODO Update with getPostByUSERID function

const UserPosts = () => {
  const [posts, setPosts] = useState([]);

  const { id } = useParams();

  const getPostFunc = () => {
    getPost(id).then(posts => setPosts(posts));
  };

  useEffect(() => {
    getPostFunc();
  }, []);

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="cards-column">
          {posts.map((post) => (
            <Post key={post.id} post={post} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default UserPosts;
