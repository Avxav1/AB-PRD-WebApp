﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="feedback" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html>
  <head>
    <title>Customer Feedback Form</title>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,300,300italic,400italic,600' rel='stylesheet' type='text/css'>
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" rel="stylesheet">
    <style>
      html, body {
      min-height: 100%;
      }
      body, div, form, input, p { 
      padding: 0;
      margin: 0;
      outline: none;
      font-family: Roboto, Arial, sans-serif;
      font-size: 14px;
      color: #666;
      line-height: 22px;
      }
      h1 {
      font-weight: 400;
      }
      h4 {
      margin: 15px 0 4px;
      }
      .testbox {
      display: flex;
      justify-content: center;
      align-items: center;
      height: inherit;
      padding: 3px;
      }
      form {
      width: 100%;
      padding: 20px;
      background: #fff;
      box-shadow: 0 2px 5px #ccc; 
      }
      input {
      width: calc(100% - 10px);
      padding: 5px;
      border: 1px solid #ccc;
      border-radius: 3px;
      vertical-align: middle;
      }
      .email {
      display: block;
      width: 45%;
      }
      input:hover, textarea:hover {
      outline: none;
      border: 1px solid #095484;
      }
      th, td {
      width: 15%;
      padding: 15px 0;
      border-bottom: 1px solid #ccc;
      text-align: center;
      vertical-align: unset;
      line-height: 18px;
      font-weight: 400;
      word-break: break-all;
      }
      .first-col {
      width: 16%;
      text-align: left;
      }
      table {
      width: 100%;
      }
      textarea {
      width: calc(100% - 6px);
      }
      .btn-block {
      margin-top: 20px;
      text-align: center;
      }
      button {
      width: 150px;
      padding: 10px;
      border: none;
      -webkit-border-radius: 5px; 
      -moz-border-radius: 5px; 
      border-radius: 5px; 
      background-color: #095484;
      font-size: 16px;
      color: #fff;
      cursor: pointer;
      }
      button:hover {
      background-color: #0666a3;
      }
      @media (min-width: 568px) {
      th, td {
      word-break: keep-all;
      }
      }
    </style>
  </head>
  <body>
    <div class="testbox">
      <form action="/">
        <h1>Customer Feedback Form</h1>
        <p>Please take a few minutes to give us feedback about our service by filling in this short Customer Feedback Form. We are conducting this research in order to measure your level of satisfaction with the quality of our service. We thank you for your participation.</p>
        <hr />
        <h3>Overall experience with our service</h3>
        <table>
          <tr>
            <th class="first-col"></th>
            <th>Very Good</th>
            <th>Good</th>
            <th>Fair</th>
            <th>Poor</th>
            <th>Very Poor</th>
          </tr>
          <tr>
            <td class="first-col">How would you rate your overall experience with our service?</td>
            <td><input type="radio" value="none" name="rate" /></td>
            <td><input type="radio" value="none" name="rate" /></td>
            <td><input type="radio" value="none" name="rate" /></td>
            <td><input type="radio" value="none" name="rate" /></td>
            <td><input type="radio" value="none" name="rate" /></td>
          </tr>
          <tr>
            <td class="first-col">How satisfied are you with the comprehensiveness of our offer?</td>
            <td><input type="radio" value="none" name="satisfied" /></td>
            <td><input type="radio" value="none" name="satisfied" /></td>
            <td><input type="radio" value="none" name="satisfied" /></td>
            <td><input type="radio" value="none" name="satisfied" /></td>
            <td><input type="radio" value="none" name="satisfied" /></td>
          </tr>
          <tr>
            <td class="first-col">How would you rate our prices?</td>
            <td><input type="radio" value="none" name="prices" /></td>
            <td><input type="radio" value="none" name="prices" /></td>
            <td><input type="radio" value="none" name="prices" /></td>
            <td><input type="radio" value="none" name="prices" /></td>
            <td><input type="radio" value="none" name="prices" /></td>
          </tr>
          <tr>
            <td class="first-col">How satisfied are you with the timeliness of order delivery?</td>
            <td><input type="radio" value="none" name="timeliness" /></td>
            <td><input type="radio" value="none" name="timeliness" /></td>
            <td><input type="radio" value="none" name="timeliness" /></td>
            <td><input type="radio" value="none" name="timeliness" /></td>
            <td><input type="radio" value="none" name="timeliness" /></td>
          </tr>
          <tr>
            <td class="first-col">How satisfied are you with the customer support?</td>
            <td><input type="radio" value="none" name="name" /></td>
            <td><input type="radio" value="none" name="name" /></td>
            <td><input type="radio" value="none" name="name" /></td>
            <td><input type="radio" value="none" name="name" /></td>
            <td><input type="radio" value="none" name="name" /></td>
          </tr>
          <tr>
            <td class="first-col">Would you recommend our product / service to other people?</td>
            <td><input type="radio" value="none" name="recommend" /></td>
            <td><input type="radio" value="none" name="recommend" /></td>
            <td><input type="radio" value="none" name="recommend" /></td>
            <td><input type="radio" value="none" name="recommend" /></td>
            <td><input type="radio" value="none" name="recommend" /></td>
          </tr>
        </table>
        <h4>What should we change in order to live up to your expectations?</h4>
        <textarea rows="5"></textarea>
        <h4>Email</h4>
        <small>Only if you want to hear more from us.</small>
        <input class="email" type="text" name="name" />
        <div class="btn-block">
          <button type="submit" href="/">Send Feedback</button>
        </div>
      </form>
    </div>
  </body>
</html>
