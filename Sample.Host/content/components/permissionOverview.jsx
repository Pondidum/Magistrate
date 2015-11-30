var PermissionOverview = React.createClass({

  getInitialState() {
    return {
      filter: "",
      permissions: []
    };
  },

  componentDidMount() {
    this.getPermissions();
  },

  getPermissions() {

    $.ajax({
      url: "/api/permissions/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          permissions: data || []
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });

  },

  onFilterChanged(value) {
    this.setState({
      filter: value
    });
  },

  onPermissionCreated(permission) {
    var newCollection = this.state.permissions.concat([permission]);

    this.setState({
      permissions: newCollection
    });
  },

  onPermissionRemoved(permission) {

    var newCollection = this.state.permissions.filter(function(p) {
      return p.key !== permission.key;
    });

    this.setState({
      permissions: newCollection
    });

  },

  render() {

    var onPermissionRemoved = this.onPermissionRemoved;
    var filter = new RegExp(this.state.filter, "i");

    var permissions = this.state.permissions
      .filter(function(permission) {
        return permission.name.search(filter) != -1;
      })
      .map(function(permission, index) {
        return (
          <PermissionTile key={index} permission={permission}>
            <RemovePermission
              permission={permission}
              onPermissionRemoved={onPermissionRemoved}
              url={"/api/permissions/" + permission.key}
              action="Delete"
              from="The System"
            />
          </PermissionTile>
        );
      });

    var create = (<CreatePermissionDialog onPermissionCreated={this.onPermissionCreated} />);

    return (
      <div>
        <ContentArea filterChanged={this.onFilterChanged} actions={[create]}>
          {permissions}
        </ContentArea>
      </div>
    );

  }

});
