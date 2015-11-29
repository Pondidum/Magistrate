var Input = ReactBootstrap.Input;

var AddPermissionsDialog = React.createClass({

  getInitialState() {
    return {
      permissions: [],
      selected: []
    };
  },

  showDialog(e) {
    e.preventDefault();
    this.getPermissions();
    this.refs.dialog.open();
  },

  getPermissions() {

    var url = "/api/permissions/all";

    $.ajax({
      url: url,
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          permissions: data || []
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(url, status, err.toString());
      }.bind(this)
    });

  },

  onSubmit() {

    var refs = this.refs;
    var dialog = refs.dialog;

    dialog.asyncStart();

    var selected = this.state.permissions.filter(function(perm) {
      return refs[perm.key].checked;
    });

  },

  render() {

    var noSelection = this.props.noSelection;

    var permissions = this.state.permissions.map(function(perm, index) {
      return (
        <tr key={index}>
          <td>
            <div className="checkbox">
              <label>
                <input type="checkbox" ref={perm.key} />
                <span className="checkbox-material"><span className="check"></span></span>
              </label>
            </div>
          </td>
          <td>{perm.name}</td>
          <td>{perm.description}</td>
        </tr>
      );
    });

    return (
      <a href="#" className={"btn btn-raised btn-default" + (noSelection ? " disabled" : "")} onClick={this.showDialog}>
        Add Permission
        <Dialog title="Select Permissions" onSubmit={this.onSubmit} ref="dialog">
          <table className="table table-hover">
            <thead>
              <tr>
                <th className="col-sm-1">#</th>
                <th className="col-sm-4">Name</th>
                <th className="col-sm-7">Description</th>
              </tr>
            </thead>
            <tbody>
              {permissions}
            </tbody>
          </table>
        </Dialog>
      </a>
    );
  }

});
