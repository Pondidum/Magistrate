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

  render() {

    var filter = new RegExp(this.state.filter, "i");

    var permissions = this.state.permissions
      .filter(function(permission) {
        return permission.name.search(filter) != -1;
      })
      .map(function(permission, index) {
        return (
          <PermissionTile key={index} permission={permission} />
        );
      });

    return (
      <div>
        <OverviewActionBar filterChanged={this.filterChanged} />
        <div className="row">
            {permissions}
        </div>
      </div>
    );

  }

});
