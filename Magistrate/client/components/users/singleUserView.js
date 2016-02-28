import React from 'react'

var SingleUserView = React.createClass({

  onNameChanged(newName) {

    var json = JSON.stringify({
      name: newName
    });

    $.ajax({
      url: this.props.url + "/name",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {
        this.props.user.name = newName;
      }.bind(this)
    });

  },

  onAdd(name, item) {
    var user = this.props.user;
    user[name] = collection.add(user[name], item);
  },

  onRemove(name, item) {
    var user = this.props.user;
    user[name] = collection.remove(user[name], item);
  },

  onChange(name, added, removed) {
    var user = this.props.user;
    user[name] = collection.change(user[name], added, removed);
  },

  render() {

    var user = this.props.user;
    var self = this;

    if (user == null)
      return (<h1>Unknown user {this.props.userKey}</h1>);

    return (
      <div className="well">
        <h1><InlineEditor initialValue={user.name} onChange={this.onNameChanged} /><small className="pull-right">{user.key}</small></h1>

        <RoleGrid
          collection={user.roles}
          onAdd={item => this.onAdd("roles", item)}
          onRemove={item => this.onRemove("roles", item)}
          onChange={(a, r) => this.onChange("roles", a, r)}
          navigate={this.props.navigate}
          url={this.props.url + "/roles/"}
          name="Roles"
          tileSize={self.props.tileSize}
        />

        <PermissionGrid
          collection={user.includes}
          onAdd={item => this.onAdd("includes", item)}
          onRemove={item => this.onRemove("includes", item)}
          onChange={(a, r) => this.onChange("includes", a, r)}
          navigate={this.props.navigate}
          url={this.props.url + "/includes/"}
          name="Includes"
          tileSize={self.props.tileSize}
        />

        <PermissionGrid
          collection={user.revokes}
          onAdd={item => this.onAdd("revokes", item)}
          onRemove={item => this.onRemove("revokes", item)}
          onChange={(a, r) => this.onChange("revokes", a, r)}
          navigate={this.props.navigate}
          url={this.props.url + "/revokes/"}
          name="Revokes"
          tileSize={self.props.tileSize}
        />

      </div>
    );
  }

});
