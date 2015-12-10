var PermissionGrid = React.createClass({

  getInitialState() {
    return {
      permissions: this.props.permissions
    };
  },

  onRemove(permission) {

    var newCollection = this.state.permissions.filter(function(p) {
      return p.key != permission.key;
    });

    this.setState({ permissions: newCollection });
  },

  onPermissionsChanged(added, removed) {

    var current = this.state.permissions;

    current = current.concat(added);
    current = current.filter(function(permission) {
      return removed.find(p => p.key == permission.key) == null;
    });

    this.setState({ permissions: current });
  },

  showPermissionsDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  render() {

    var self = this;

    var permissions = this.state.permissions.map(function(permission, index) {
      return (
        <li key={index} className="col-md-3">
          <PermissionTile
            content={permission}
            onRemove={self.onRemove}
            navigate={self.props.navigate}
            url={self.props.url}
            showEdit={false}
          />
        </li>
      );
    });

    return (
      <div>

        <div className="page-header">
          <a href="#" onClick={this.showPermissionsDialog}>Change Permissions...</a>

          <PermissionSelector
            initialValue={this.state.permissions}
            url={self.props.url}
            onChange={this.onPermissionsChanged}
            ref="dialog"
          />

        </div>

        <ul className="list-unstyled list-inline row">
          {permissions}
        </ul>

      </div>
    );

  }

});
