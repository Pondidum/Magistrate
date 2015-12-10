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
            initialValue={permissions}
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
