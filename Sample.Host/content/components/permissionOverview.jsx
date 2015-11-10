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

  filterChanged(value) {
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
    //?
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
          <li key={index} className="col-sm-3 ">
            <PermissionPill key={index} permission={permission}>
              <RemovePermission
                permission={permission}
                onPermissionRemoved={onPermissionRemoved}
                url={"/api/permissions/" + permission.key + "/removePermission/"}
                action="Delete"
                from="The System"
              />
            </PermissionPill>
          </li>
        );
      });

    return (
      <div>
        <OverviewActionBar filterChanged={this.filterChanged}>
          <li><CreatePermissionDialog onPermissionCreated={this.onPermissionCreated} /></li>
        </OverviewActionBar>
        <div className="row">
          <ul className="list-unstyled list-inline">
            {permissions}
          </ul>
        </div>
      </div>
    );

  }

});
