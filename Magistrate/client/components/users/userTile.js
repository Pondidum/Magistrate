import React from 'react'
import Tile from '../tile'

var UserTile = React.createClass({

  navigateToDetails(e) {
    e.preventDefault();
    this.props.navigate("singleuser", { key: this.props.content.key});
  },

  onDelete(success, error) {

    var self = this;
    var user = this.props.content;

    $.ajax({
      url: this.props.url,
      method: 'DELETE',
      cache: false,
      data: JSON.stringify([ user.key ]),
      success: function() {
        success();
        self.props.onRemove(user);
      },
      error: error
    });

  },

  render() {

    var user = this.props.content;

    var confirmation = (
      <p>Are you sure you want to delete the user <strong>{user.name}</strong>?</p>
    );

    return (
      <Tile
        title={user.name}
        navigateTo={this.navigateToDetails}
        onDelete={this.onDelete}
        dialogContent={confirmation}
        tileSize={this.props.tileSize}>
        <span>Roles: {user.roles.length}</span>
        <span>Includes: {user.includes.length}</span>
        <span>Revokes: {user.revokes.length}</span>
      </Tile>
    );
  }
});

export default UserTile