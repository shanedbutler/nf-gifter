import React from "react";
import { Card, CardImg, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const Post = ({ post }) => {
  return (
    <Card className="m-4">
        <div className="text-left px-2">
          Posted by:
          <Link to={`/users/${post.userProfile.id}`}>
            {post.userProfile.name}
          </Link>
        </div>
      <CardImg top src={post.imageUrl} alt={post.title} />
      <CardBody>
        <Link to={`/posts/${post.id}`}>
          <strong>{post.title}</strong>
        </Link>
        <p>{post.caption}</p>
      </CardBody>
    </Card>
  );
};

export default Post;
