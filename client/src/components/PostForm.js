import React, { useState } from "react"
import { addPost } from "../modules/PostManager";

export const PostForm = () => {

    const [title, setTitle] = useState("");
    const [caption, setCaption] = useState("");
    const [url, setUrl] = useState("");

    const post = {
        title: title,
        caption: caption,
        dateCreated: new Date(),
        imageUrl: url,
        userProfileId: 1
    };

    const handleClick = () => {
        addPost(post);
    }
    
    return (
    <>
        <h4>New Post</h4>
        <label htmlFor="title">Title:</label>
        <input property="title" onChange={e => setTitle(e.target.value)} /><br/>
        <label htmlFor="caption">Caption:</label>
        <input property="caption" onChange={e => setCaption(e.target.value)} /><br/>
        <label htmlFor="url">URL:</label>
        <input property="url" onChange={e => setUrl(e.target.value)} /><br/>
        <button onClick={handleClick}>Submit</button>
        <br/>
    </>
    );
};